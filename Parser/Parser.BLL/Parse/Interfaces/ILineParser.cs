using Parser.BLL.Models;

namespace Parser.BLL.Parse.Interfaces
{
    public interface ILineParser
    {
        LogLineModel ParseLine(string line);
    }
}
