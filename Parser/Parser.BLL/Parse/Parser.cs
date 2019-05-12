using Parser.BLL.Models;
using Parser.BLL.Parse.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.BLL.Parse
{
    public class Parser: IParser
    {
        private ILineParser lineParser { get; set; }

        public Parser(ILineParser lineParser)
        {
            this.lineParser = lineParser;
        }
        public List<LogLineModel> ParseAsync(IEnumerable<string> fileLines)
        {
            var lines = new List<LogLineModel>();
            foreach (var line in fileLines)
            {
                lines.Add(lineParser.ParseLineAsync(line));
            }
            return lines;
        }
    }
}
