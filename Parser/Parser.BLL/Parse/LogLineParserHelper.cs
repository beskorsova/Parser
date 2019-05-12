using Newtonsoft.Json.Linq;
using Parser.BLL.Models;
using Parser.BLL.Options;
using Parser.BLL.Parse.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;

namespace Parser.BLL.Parse
{
    public class LogLineParserHelper: ILogLineParserHelper
    {
        private GeolocationOptions geolocationOptions { get; set; }
        public LogLineParserHelper(GeolocationOptions geolocationOptions)
        {
            this.geolocationOptions = geolocationOptions;
        }
        public void SetGeolocation(LogLineModel logLine)
        {
            Dns.BeginGetHostAddresses(logLine.Host, async (x) =>
            {
                try
                {
                    var ip = Dns.EndGetHostAddresses(x)[0]?.MapToIPv6().ToString();

                    using (var client = new WebClient())
                    {
                        var uri = geolocationOptions.AccessKey == "" ?
                        String.Format(geolocationOptions.Uri, ip) :
                        String.Format(geolocationOptions.Uri, ip, geolocationOptions.AccessKey);
                        var json = await client.DownloadStringTaskAsync(
                            new Uri(uri));
                        logLine.Country = JObject.Parse(json)[geolocationOptions.GeolocationFieldName].ToString();
                    }
                }
                catch(SocketException) { }
            }, null);
        }
    }
}
