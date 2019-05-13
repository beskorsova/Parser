using Parser.BLL.Models;
using Parser.BLL.Parse.Interfaces;
using System.Collections.Generic;
using System.Threading;

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
            List<Thread> threadsToWait = new List<Thread>();
            foreach (var line in fileLines)
            {
                var tuple = lineParser.ParseLine(line);
                if (tuple.Item1 != null)
                {
                    result.Add(tuple.Item1);
                }
            }
            foreach(var thread in threadsToWait)
            {
                thread.Join();
            }
            return result;
        }
    }
}
