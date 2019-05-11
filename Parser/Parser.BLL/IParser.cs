using Parser.BLL.Models;
using System.Collections.Generic;

namespace Parser.BLL
{
    public interface IParser
    {
        IEnumerable<LogLineModel> Parse(IEnumerable<string> fileLines);
    }
}
