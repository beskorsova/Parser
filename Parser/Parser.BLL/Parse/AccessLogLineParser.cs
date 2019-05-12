using Parser.BLL.Models;
using Parser.BLL.Options;
using Parser.BLL.Parse.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Parser.BLL.Parse
{
    public class AccessLogLineParser : ILineParser
    {
        private ExcludeRuleOptions excludedRuleOptions;
        private ILogLineParserHelper logLineParserHelper;
        private readonly string dateFormat = "dd/MMM/yyyy:HH:mm:ss zzz";
        
        protected class LineIndexer
        {
            public int StartIndex { get; private set; }
            public int EndIndex { get; private set; }
            public int Length { get => EndIndex - StartIndex; }
            public bool FindEndIndex(string line, string part)
            {
                var index = this.Find(line, part, this.StartIndex);
                if (index == -1)
                {
                    return false;
                }
                this.EndIndex = index;
                return true;
            }
            public bool FindStartIndex(string line, string part, int? startIncreaser = null)
            {
                var index = this.Find(line, part, this.EndIndex);
                if (index == -1)
                {
                    return false;
                }
                this.StartIndex = index + (startIncreaser ?? part.Length);
                return true;
            }
            private int Find(string line, string part, int fromIndex)
            {
                return line.IndexOf(part, fromIndex);
            }
        }

        protected class LinePartParser
        {
            public Action<string, LogLineModel> Parser { get; private set; }
            public Func<LogLineModel, bool> Filter { get; private set; }
            public LinePartParser(Action<string, LogLineModel> parser, Func<LogLineModel, bool> filter = null)
            {
                this.Parser = parser;
                this.Filter = filter;
            }
        }

        protected Dictionary<LinePartEnum, LinePartParser> linePartParsers
            = new Dictionary<LinePartEnum, LinePartParser>();

        protected Dictionary<int, LinePartEnum> linePartIndicators = new Dictionary<int, LinePartEnum>();
        protected LineIndexer indexer;

        public AccessLogLineParser(ILogLineParserHelper logLineParserHelper,
            ExcludeRuleOptions excludedRuleOptions)
        {
            this.excludedRuleOptions = excludedRuleOptions;
            this.logLineParserHelper = logLineParserHelper;

            this.linePartParsers.Add(LinePartEnum.Host, new LinePartParser((line, model) =>
            {
                indexer.FindEndIndex(line, " ");
                model.Host = line.Substring(indexer.StartIndex, this.indexer.Length);
                this.logLineParserHelper.SetGeolocation(model);
            }));

            this.linePartParsers.Add(LinePartEnum.Date, new LinePartParser((line, model) =>
            {
                indexer.FindStartIndex(line, " - - [");
                indexer.FindEndIndex(line, "]");
                model.Date = DateTime.ParseExact(
                    line.Substring(indexer.StartIndex, indexer.Length), this.dateFormat,
                    CultureInfo.InvariantCulture);
            }));

            this.linePartParsers.Add(LinePartEnum.Route, new LinePartParser(
                (line, model) =>
                {
                    if (!indexer.FindStartIndex(line, " /", 1)) return;
                    if (!indexer.FindEndIndex(line, "?"))
                    {
                        indexer.FindEndIndex(line, " ");
                    }
                    model.Route = line.Substring(indexer.StartIndex, indexer.Length);
                },
                (model) =>
                {
                    if (model.Route == null) return true;
                    foreach (var excludedRoute in this.excludedRuleOptions.Routes)
                    {
                        if (model.Route.EndsWith(excludedRoute))
                            return false;
                    }
                    return true;
                }));

            this.linePartParsers.Add(LinePartEnum.Parameters, new LinePartParser((line, model) =>
            {
                if (!indexer.FindStartIndex(line, "?"))
                {
                    return;
                }
                indexer.FindEndIndex(line, " ");
                var pairs = line.Substring(indexer.StartIndex, indexer.Length).Split('&');
                foreach (var p in pairs)
                {
                    var i = p.IndexOf('=');
                    model.Parameters.Add(p.Substring(0, i), p.Substring(i + 1));
                }
            }));

            this.linePartParsers.Add(LinePartEnum.StatusResult, new LinePartParser((line, model) =>
            {
                indexer.FindStartIndex(line, "\" ");
                indexer.FindEndIndex(line, " ");
                model.StatusResult = int.Parse(line.Substring(indexer.StartIndex, indexer.Length));
            }));

            this.linePartParsers.Add(LinePartEnum.BytesSent, new LinePartParser((line, model) =>
            {
                indexer.FindStartIndex(line, " ");
                var bytesSent = 0;
                if (int.TryParse(line.Substring(indexer.StartIndex), out bytesSent))
                {
                    model.BytesSent = bytesSent;
                }
            }));

            this.linePartIndicators.Add(0, LinePartEnum.Host);
            this.linePartIndicators.Add(1, LinePartEnum.Date);
            this.linePartIndicators.Add(2, LinePartEnum.Route);
            this.linePartIndicators.Add(3, LinePartEnum.Parameters);
            this.linePartIndicators.Add(4, LinePartEnum.StatusResult);
            this.linePartIndicators.Add(5, LinePartEnum.BytesSent);
        }

        public LogLineModel ParseLine(string line)
        {
            var result = new LogLineModel();
            indexer = new LineIndexer();
            for (int i = 0; i <= this.linePartIndicators.Max(x => x.Key); ++i)
            {
                var linePart = this.linePartIndicators.Where(x => x.Key == i).First().Value;
                linePartParsers[linePart].Parser?.Invoke(line, result);
                if (linePartParsers[linePart].Filter == null) continue;
                if (!linePartParsers[linePart].Filter.Invoke(result))
                {
                    result = null;
                    break;
                }
            }
            return result;
        }
    }
}
