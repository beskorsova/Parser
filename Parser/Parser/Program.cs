using Parser.BLL;
using Microsoft.Extensions.DependencyInjection;
using Parser.Data.Core.DataAccess;
using Parser.Configuration;
using System;
using Parser.Data.Core.Entities;

namespace Parser
{
    class Program
    {
        static IServiceProvider serviceProvider;

        static void Setup()
        {
            serviceProvider = new DependencyResolver().ServiceProvider;
        }

        static async System.Threading.Tasks.Task Main(string[] args)
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
            using (serviceProvider.CreateScope())
            {
                var repository = serviceProvider.GetService<IAsyncRepository>();
                await repository.AddAsync(new LogLine()
                {
                    BytesSent = 1,
                    Date = DateTime.Now,
                    Host = "1",
                    Route = "1"
                });
            }

        }
    }
}
