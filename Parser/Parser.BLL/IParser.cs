using Parser.BLL.Models;
using System.Collections.Generic;

namespace Parser.BLL
{
    public interface IParser
    {
        IEnumerable<LogLine> Parse(IEnumerable<string> fileLines);
    }
}
