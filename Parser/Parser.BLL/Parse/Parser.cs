using Parser.BLL.Models;
using Parser.BLL.Parse.Interfaces;
using System.Collections.Generic;

namespace Parser.BLL.Parse
{
    public class Parser: IParser
    {
        private LogLineParserBase lineParser { get; set; }

        public Parser(LogLineParserBase lineParser)
        {
            this.lineParser = lineParser;
        }
        public List<LogLineModel> Parse(IEnumerable<string> fileLines)
        {
            var result = new List<LogLineModel>();
            foreach (var line in fileLines)
            {
                var parsedLine = lineParser.ParseLine(line);
                if (parsedLine != null)
                {
                    result.Add(parsedLine);
                }
            }
            return result;
        }
    }
}
