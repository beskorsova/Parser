using Parser.BLL;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            LogService logService = new LogService();
            var log = logService.ReadLog("access_log");
            var parser = new BLL.Parser(new AccessLogLineParser());
            var logLines = parser.Parse(log);
            foreach(var line in logLines)
            {
                if(line!=null)
                {
                    System.Console.WriteLine(line.Host);
                }
            }
        }
    }
}
