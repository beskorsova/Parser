using Parser.BLL.Models;

namespace Parser.BLL
{
    public interface ILineParser
    {
        LogLineModel ParseLine(string line);
    }
}
