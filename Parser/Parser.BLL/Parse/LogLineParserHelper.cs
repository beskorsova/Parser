using Newtonsoft.Json.Linq;
using Parser.BLL.Models;
using Parser.BLL.Parse.Interfaces;
using System;
using System.Net;

namespace Parser.BLL.Parse
{
    public class LogLineParserHelper: ILogLineParserHelper
    {
        public void SetGeolocation(LogLineModel logLine)
        {
            Dns.BeginGetHostAddresses(logLine.Host, async (x) =>
            {
                try
                {
                    var ip = Dns.EndGetHostAddresses(x)[0]?.MapToIPv6().ToString();

                    using (var client = new WebClient())
                    {
                        var json = await client.DownloadStringTaskAsync(new Uri($"http://api.ipstack.com/{ip}?access_key=206b55680e755639e267360b4f15ba1f&format=1"));
                        logLine.Country = JObject.Parse(json)["country_name"].ToString();
                    }

                }
                catch { }
            }, null);
        }
    }
}
