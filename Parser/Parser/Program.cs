using Parser.BLL;
using Microsoft.Extensions.DependencyInjection;
using Parser.Data.Core.DataAccess;
using Parser.Data.DataAccess;
using Parser.Data.Core.Entities;
using Parser.Data;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Parser
{
    class Program
    {
        static ServiceProvider serviceProvider;
        static void Setup()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddUserSecrets(Assembly.GetExecutingAssembly())
               .Build();

            var services = new ServiceCollection();

            services.
                AddDbContext<ParserDbContext>(
                    x => x.UseSqlServer(configuration.GetConnectionString("Default"))
                ).
                AddTransient<IParser, BLL.Parser>().
                AddTransient<ILineParser, AccessLogLineParser>(x => new AccessLogLineParser(
                    new[] { ".jpg", ".gif", ".png", ".css", ".js" })).
                AddTransient<ILogService, LogService>();

            services.AddScoped<IAsyncRepository, AsyncRepository>();

            serviceProvider = services.BuildServiceProvider();
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

        }
    }
}
