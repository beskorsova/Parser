using Parser.Data.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Data.Core.DataAccess
{
    public interface ILogLineRepository
    {
        Task<List<LogLine>> GetTopHosts(int n, DateTime start, DateTime end, CancellationToken token = default(CancellationToken));
        Task<List<LogLine>> GetTopRoutes(int n, DateTime start, DateTime end, CancellationToken token = default(CancellationToken));
        Task<List<LogLine>> GetAll(DateTime start, DateTime end, int offset, int limit = 10, CancellationToken token = default(CancellationToken));
    }
}
