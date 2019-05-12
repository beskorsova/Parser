﻿using Newtonsoft.Json.Linq;
using Parser.BLL.Models;
using Parser.BLL.Options;
using Parser.BLL.Parse.Interfaces;
using System;
using System.Globalization;
using System.Net;

namespace Parser.BLL.Parse
{
    public class AccessLogLineParser: ILineParser
    {
        private ExcludeRule excludedRule;
        private ILogLineParserHelper logLineParserHelper;

        public AccessLogLineParser(ILogLineParserHelper logLineParserHelper,
            ExcludeRule excludedRule)
        {
            this.excludedRule = excludedRule;
            this.logLineParserHelper = logLineParserHelper;
        }
        public LogLineModel ParseLine(string line)
        {
            var result = new LogLineModel();
            var startIndex = 0;
            var endIndex = line.IndexOf(' ');
            result.Host = line.Substring(startIndex, endIndex - startIndex);
            this.logLineParserHelper.SetGeolocation(result);
            startIndex = line.IndexOf(" - - [", endIndex) + 6;
            endIndex = line.IndexOf("]", startIndex);
            result.Date = DateTime.ParseExact(line.Substring(startIndex, endIndex - startIndex), "dd/MMM/yyyy:HH:mm:ss zzz", CultureInfo.InvariantCulture)
                .ToUniversalTime();
            
            startIndex = line.IndexOf(" /", endIndex);

            if (startIndex != -1)
            {
                startIndex += 1;
                endIndex = line.IndexOf('?', startIndex);
                if (endIndex == -1)
                {
                    endIndex = line.IndexOf(' ', startIndex);
                    result.Route = line.Substring(startIndex, endIndex - startIndex);
                }
                else
                {
                    result.Route = line.Substring(startIndex, endIndex - startIndex);

                    startIndex = endIndex + 1;
                    endIndex = line.IndexOf(' ', startIndex);
                    var pairs = line.Substring(startIndex, endIndex - startIndex).Split('&');
                    foreach (var p in pairs)
                    {
                        var i = p.IndexOf('=');
                        result.Parameters.Add(p.Substring(0, i), p.Substring(i + 1));
                    }
                }

                foreach (var excludedRoute in this.excludedRule.Routes)
                {
                    if (result.Route.EndsWith(excludedRoute))
                        return null;
                }
            }

            startIndex =  line.IndexOf("\" ", endIndex) + 2;
            endIndex = line.IndexOf(' ', startIndex);
            result.StatusResult = int.Parse(line.Substring(startIndex, endIndex - startIndex));

            startIndex = line.IndexOf(' ', endIndex);
            int bytesSent = 0;
            if (int.TryParse(line.Substring(startIndex), out bytesSent))
            {
                result.BytesSent = bytesSent;
            }
            return result;
        }
    }
}
