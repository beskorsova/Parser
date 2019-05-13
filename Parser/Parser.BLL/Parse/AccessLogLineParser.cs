using Parser.BLL.Parse.Interfaces;
using System;
using System.Globalization;

namespace Parser.BLL.Parse
{
    public class AccessLogLineParser : LineParserBase
    {
        private ILogLineParserHelper logLineParserHelper;
        private readonly string dateFormat = "dd/MMM/yyyy:HH:mm:ss zzz";
        
        public AccessLogLineParser(ILogLineParserHelper logLineParserHelper)
        {
            this.logLineParserHelper = logLineParserHelper;

            this.linePartParsers.Add(LinePartEnum.Host, new LinePartParser((line, model) =>
            {
                this.Indexer.FindEndIndex(line, " ");
                model.Host = line.Substring(this.Indexer.StartIndex, this.Indexer.Length);
                this.logLineParserHelper.SetGeolocation(model);
            }));

            this.linePartParsers.Add(LinePartEnum.Date, new LinePartParser((line, model) =>
            {
                this.Indexer.FindStartIndex(line, " - - [");
                this.Indexer.FindEndIndex(line, "]");
                model.Date = DateTime.ParseExact(
                    line.Substring(this.Indexer.StartIndex, this.Indexer.Length), this.dateFormat,
                    CultureInfo.InvariantCulture);
            }));

            this.linePartParsers.Add(LinePartEnum.Route, new LinePartParser(
                (line, model) =>
                {
                    if (!this.Indexer.FindStartIndex(line, " /", 1)) return;
                    if (!this.Indexer.FindEndIndex(line, "?"))
                    {
                        this.Indexer.FindEndIndex(line, " ");
                    }
                    model.Route = line.Substring(this.Indexer.StartIndex, this.Indexer.Length);
                },
                (model) =>
                {
                    if (model.Route == null) return true;
                    return this.logLineParserHelper.CheckRoute(model);
                }));

            this.linePartParsers.Add(LinePartEnum.Parameters, new LinePartParser((line, model) =>
            {
                if (!this.Indexer.FindStartIndex(line, "?"))
                {
                    return;
                }
                this.Indexer.FindEndIndex(line, " ");
                var pairs = line.Substring(this.Indexer.StartIndex, this.Indexer.Length).Split('&');
                foreach (var p in pairs)
                {
                    var i = p.IndexOf('=');
                    model.Parameters.Add(p.Substring(0, i), p.Substring(i + 1));
                }
            }));

            this.linePartParsers.Add(LinePartEnum.StatusResult, new LinePartParser((line, model) =>
            {
                this.Indexer.FindStartIndex(line, "\" ");
                this.Indexer.FindEndIndex(line, " ");
                model.StatusResult = int.Parse(line.Substring(this.Indexer.StartIndex, this.Indexer.Length));
            }));

            this.linePartParsers.Add(LinePartEnum.BytesSent, new LinePartParser((line, model) =>
            {
                this.Indexer.FindStartIndex(line, " ");
                var bytesSent = 0;
                if (int.TryParse(line.Substring(this.Indexer.StartIndex), out bytesSent))
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
    }
}
