using Parser.BLL.Models;
using System.Threading.Tasks;

namespace Parser.BLL.Services.Interfaces
{
    public interface IParserStoreService
    {
        Task CreateAsync(LogLineModel logLineDto);
    }
}
