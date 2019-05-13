using Parser.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Core.Interfaces
{
    public interface ILogLineService
    {
        Task<List<string>> GetTopHosts(TopFilterDataModel filter, CancellationToken token = default(CancellationToken));
        Task<List<string>> GetTopRoutes(TopFilterDataModel filter, CancellationToken token = default(CancellationToken));
        Task<List<LogLineDataModel>> GetAll(TableFilterDataModel filter, CancellationToken token = default(CancellationToken));

    }
}
