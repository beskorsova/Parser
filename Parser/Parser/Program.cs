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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var logService = serviceProvider.GetService<ILogService>();
            var parser = serviceProvider.GetService<IParser>();
            var log = logService.ReadLog("access_log");
            var logLines = parser.Parse(log);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);
            foreach (var line in logLines)
            {
                if (line != null)
                {
                    System.Console.WriteLine(line);
                }
            }
        }
    }
}
