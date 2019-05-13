using Microsoft.Extensions.DependencyInjection;
using Parser.Configuration;
using System;
using Parser.BLL.Parse.Interfaces;
using Parser.BLL.Services.Interfaces;

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
            Console.WriteLine("Please enter file path");
            var filePath = Console.ReadLine();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var logService = serviceProvider.GetService<ILogService>();
            var parser = serviceProvider.GetService<IParser>();
            var log = logService.ReadLog(filePath);
            var logLines = parser.Parse(log);

            System.Console.WriteLine($"Parsing: {watch.ElapsedMilliseconds} ElapsedMilliseconds");

            var logLineService = serviceProvider.GetService<IParserStoreService>();
            using (serviceProvider.CreateScope())
            {
                await logLineService.CreateAsync(logLines);
            }

            System.Console.WriteLine($"Saving: {watch.ElapsedMilliseconds} ElapsedMilliseconds");

        }
    }
}
