using Parser.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Core.Interfaces
{
    public interface ILogLineService
    {
        Task<List<string>> GetTopHosts(int n, DateTime? start, DateTime? end, CancellationToken token = default(CancellationToken));
        Task<List<string>> GetTopRoutes(int n, DateTime? start, DateTime? end, CancellationToken token = default(CancellationToken));
        Task<List<LogLineDataModel>> GetAll(DateTime? start, DateTime? end, int offset, int limit = 10, CancellationToken token = default(CancellationToken));

    }
}
