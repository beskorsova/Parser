using Parser.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser.BLL.Parse
{
    public abstract class LogLineParserBase
    {
        // Operating with line - defines indexes where log value is
        protected class LineIndexer
        {
            public int StartIndex { get; private set; }
            public int EndIndex { get; private set; }
            public int Length { get => EndIndex - StartIndex; }

            private int Find(string line, string part, int fromIndex)
            {
                return line.IndexOf(part, fromIndex);
            }
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
            public void Reset()
            {
                this.StartIndex = 0;
                this.EndIndex = 0;
            }
        }

        
        protected class LinePartParser
        {
            // Get initial string and sets model value for a field
            public Action<string, LogLineModel> Parser { get; private set; }
            
            // Checks if model value fits accepted rules
            public Func<LogLineModel, bool> Filter { get; private set; }
            public LinePartParser(Action<string, LogLineModel> parser, Func<LogLineModel, bool> filter = null)
            {
                this.Parser = parser;
                this.Filter = filter;
            }
        }

        // Parsers and filters for each log line part
        protected readonly Dictionary<LinePartEnum, LinePartParser> linePartParsers;

        // Indicator of order in log line for each value
        protected readonly Dictionary<int, LinePartEnum> linePartIndicators;
        
        // Store for current indexes when parsing line
        protected LineIndexer Indexer { get; private set; } = new LineIndexer();

        protected LogLineParserBase()
        {
            linePartParsers = new Dictionary<LinePartEnum, LinePartParser>();
            linePartIndicators = new Dictionary<int, LinePartEnum>();
        }

        public LogLineModel ParseLine(string line)
        {
            var result = new LogLineModel();
            
            // Set indexes to 0
            Indexer.Reset();
            
            // Get each log value in order it is contained in line
            for (int i = 0; i <= this.linePartIndicators.Max(x => x.Key); ++i)
            {
                // Indicate witch line part is next in line
                var linePart = this.linePartIndicators.Where(x => x.Key == i).First().Value;
                
                // Parse line to get value
                linePartParsers[linePart].Parser?.Invoke(line, result);
                if (linePartParsers[linePart].Filter == null) continue;

                // Checks if the value is acceptable
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
