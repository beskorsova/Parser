using Parser.BLL.Models;

namespace Parser.BLL.Parse.Interfaces
{
    public interface ILogLineParserHelper
    {
        void SetGeolocation(LogLineModel logLine);
    }
}
