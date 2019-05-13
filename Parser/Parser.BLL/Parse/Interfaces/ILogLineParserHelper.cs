using Parser.BLL.Models;
using System.Threading;

namespace Parser.BLL.Parse.Interfaces
{
    public interface ILogLineParserHelper
    {
        Thread SetGeolocation(LogLineModel logLine, CancellationTokenSource cts);
        bool CheckRoute(LogLineModel logLine);
    }
}
