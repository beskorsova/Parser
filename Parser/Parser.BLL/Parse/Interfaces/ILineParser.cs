using Parser.BLL.Models;
using System.Threading.Tasks;

namespace Parser.BLL.Parse.Interfaces
{
    public interface ILineParser
    {
        LogLineModel ParseLineAsync(string line);
    }
}
