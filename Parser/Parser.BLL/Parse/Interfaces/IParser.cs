using Parser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.BLL.Parse.Interfaces
{
    public interface IParser
    {
        List<LogLineModel> ParseAsync(IEnumerable<string> fileLines);
    }
}
