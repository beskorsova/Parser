﻿using Parser.BLL;
using Microsoft.Extensions.DependencyInjection;
using Parser.Configuration;
using System;
using Parser.BLL.Parse.Interfaces;

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

        }
    }
}
