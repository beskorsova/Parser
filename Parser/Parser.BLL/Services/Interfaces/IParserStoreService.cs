using Parser.BLL.DTO;
using System.Threading.Tasks;

namespace Parser.BLL.Services.Interfaces
{
    public interface IParserStoreService
    {
        Task CreateAsync(LogLineDto logLineDto);
    }
}
