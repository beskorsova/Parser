using Parser.BLL.Models;
using System;
using System.Globalization;

namespace Parser.BLL
{
    public class AccessLogLineParser: ILineParser
    {
        private string[] excludedRoutes { get; set; }
        public AccessLogLineParser(string[] excludedRoutes)
        {
            this.excludedRoutes = excludedRoutes;
        }
        public LogLine ParseLine(string line)
        {
            var result = new LogLine();
            var startIndex = 0;
            var endIndex = line.IndexOf(' ');
            result.Host = line.Substring(startIndex, endIndex - startIndex);

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

                foreach (var excludedRoute in this.excludedRoutes)
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
