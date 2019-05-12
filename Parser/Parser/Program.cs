using Microsoft.Extensions.DependencyInjection;
using Parser.Configuration;
using System;
using Parser.BLL.Parse.Interfaces;
using Parser.BLL.Services.Interfaces;
using System.Linq;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var logLines = parser.ParseAsync(log);

            System.Console.WriteLine(watch.ElapsedMilliseconds);

            var logLineService = serviceProvider.GetService<IParserStoreService>();
            using (serviceProvider.CreateScope())
            {
               var threads = new List<Thread>();
               foreach(var logLine in logLines.Where(x=>x!=null))
                {
                    var thread = new Thread(() =>
                    {
                        try
                        {
                            var ip = Dns.GetHostAddresses(logLine.Host)[0].MapToIPv4().ToString();
                            using (var client = new WebClient())
                            {
                                var json = client.DownloadString($"http://api.ipstack.com/{ip}?access_key=0b4d5e049204104a60c1fba59c2b23a6&format=1");
                                logLine.Country = JObject.Parse(json)["country_name"].ToString();
                            }
                        }
                        catch
                        {
                        }
                    });
                   // thread.Start();

                    //foreach (var th in threads)
                    //{
                    //    th.Join();
                    //}

                }
              
                await logLineService.CreateAsync(logLines.Where(x=>x!=null).ToList());
            }

            System.Console.WriteLine(watch.ElapsedMilliseconds);

        }
    }
}
