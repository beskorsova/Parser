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
            result.Host = line.Substring(0, line.IndexOf(' '));

            line = line.Substring(line.IndexOf(" - - [") + 6);
            result.Date = DateTime.ParseExact(line.Substring(0, line.IndexOf("]")), "dd/MMM/yyyy:HH:mm:ss zzz", CultureInfo.InvariantCulture).ToUniversalTime();

            line = line.Substring(line.IndexOf("]"));
            var index = line.IndexOf(" /");
            if (index != -1)
            {
                line = line.Substring(index + 1);
                index = line.IndexOf('?');
                result.Route = index != -1 ? line.Substring(0, index) : line.Substring(0, line.IndexOf(' '));

                foreach (var excludedRoute in this.excludedRoutes)
                {
                    if (result.Route.EndsWith(excludedRoute))
                        return null;
                }
                if (index != -1)
                {
                    line = line.Substring(index + 1);
                    var pairs = line.Substring(0, line.IndexOf(' ')).Split('&');
                    foreach (var p in pairs)
                    {
                        var i = p.IndexOf('=');
                        result.Parameters.Add(p.Substring(0, i), p.Substring(i + 1));
                    }
                }
            }

            line = line.Substring(line.IndexOf("\" ") + 2);
            result.StatusResult = int.Parse(line.Substring(0, line.IndexOf(' ')));

            line = line.Substring(line.IndexOf(' '));
            int bytesSent = 0;
            if (int.TryParse(line, out bytesSent))
            {
                result.BytesSent = bytesSent;
            }
            return result;
        }
    }
}
