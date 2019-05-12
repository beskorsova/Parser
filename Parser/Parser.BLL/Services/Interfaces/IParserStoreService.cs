using Parser.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.BLL.Services.Interfaces
{
    public interface IParserStoreService
    {
        Task CreateAsync(IEnumerable<Task<LogLineModel>> logLineModel);
        void Create(IEnumerable<Task<LogLineModel>> logLineModel);
        void Create(List<LogLineModel> logLineModel);
        void Create(LogLineModel logLineModel);
        void SaveChanges();
    }
}
