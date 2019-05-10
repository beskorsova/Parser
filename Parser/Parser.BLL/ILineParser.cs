using Parser.BLL.Models;

namespace Parser.BLL
{
    public interface ILineParser
    {
        LogLine ParseLine(string line);
    }
}
