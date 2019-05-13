using Parser.Data.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Data.Core.DataAccess
{
    public interface ILogLineRepository
    {
        Task<List<LogLine>> GetTop(int n, DateTime start, DateTime end, CancellationToken token = default(CancellationToken));
    }
}
