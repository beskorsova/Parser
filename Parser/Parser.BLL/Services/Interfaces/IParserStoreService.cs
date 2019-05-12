using Parser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.BLL.Services.Interfaces
{
    public interface IParserStoreService
    {
        Task CreateAsync(List<LogLineModel> logLineModel);
    }
}
