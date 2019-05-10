using Parser.BLL;
using Microsoft.Extensions.DependencyInjection;

namespace Parser
{
    class Program
    {
        static ServiceProvider serviceProvider;
        static void Setup()
        {
            serviceProvider = new ServiceCollection().
            AddTransient<IParser, BLL.Parser>().
            AddTransient<ILineParser, AccessLogLineParser>(x => new AccessLogLineParser(
                new[] { ".jpg", ".gif", ".png", ".css", ".js" })).
            AddTransient<ILogService, LogService>().BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Setup();
            var logService = serviceProvider.GetService<ILogService>();
            var log = logService.ReadLog("access_log");
            var parser = serviceProvider.GetService<IParser>();
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
