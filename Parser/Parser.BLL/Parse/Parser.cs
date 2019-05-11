using Parser.BLL.Models;
using Parser.BLL.Parse.Interfaces;
using System.Collections.Generic;

namespace Parser.BLL.Parse
{
    public class Parser: IParser
    {
        private ILineParser lineParser { get; set; }

        public Parser(ILineParser lineParser)
        {
            this.lineParser = lineParser;
        }
        public IEnumerable<LogLineModel> Parse(IEnumerable<string> fileLines)
        {
            foreach(var line in fileLines)
            {
                yield return lineParser.ParseLine(line);
            }
        }
    }
}
