using Dapper;
using Parser.Data.Core.DataAccess;
using Parser.Data.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Data.Core.Infrastructure
{
    public class LogLineRepository : AsyncRepository, ILogLineRepository
    {
        public LogLineRepository(IParserConnection connection) : base(connection)
        {
        }

        public Task<List<LogLine>> GetAll(DateTime start, DateTime end, int offset, int limit = 10, CancellationToken token = default(CancellationToken))
        {

            var lookup = new Dictionary<long, LogLine>();
            return this.RunAsync(async (dbConnection) =>
               (await dbConnection.QueryAsync<LogLine, QueryParameter, LogLine>
                 (
                     "SELECT l.*, qp.* FROM LogLines l inner join QueryParameters qp on l.Id = qp.LogLineId ORDER BY l.Date ASC " +
                     "OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY",
                      (l, qp) => {
                         LogLine logLine;
                         if (!lookup.TryGetValue(l.Id, out logLine))
                         {
                              lookup.Add(l.Id, logLine = l);
                         }
                         logLine.Parameters.Add(qp);
                         return logLine;
                     }, new { Offset = offset, Limit = limit })).ToList());
        }

        public Task<List<LogLine>> GetTopHosts(int n, DateTime start, DateTime end,
            CancellationToken token = default(CancellationToken))
        {
            return this.RunAsync(async (dbConnection) =>
              (await dbConnection.QueryAsync<LogLine>
                (new CommandDefinition(
                    "SELECT TOP (@N) l.Host FROM LogLines l WHERE Date >= @Start AND Date <= @End " +
                    "GROUP BY l.Host ORDER BY COUNT(*) DESC",
                    new { N = n, Start = start, End = end },
                    cancellationToken: token))).ToList());
        }

        public Task<List<LogLine>> GetTopRoutes(int n, DateTime start, DateTime end, CancellationToken token = default(CancellationToken))
        {
            return this.RunAsync(async (dbConnection) =>
              (await dbConnection.QueryAsync<LogLine>
                (new CommandDefinition(
                    "SELECT TOP (@N) l.Route FROM LogLines l WHERE Date >= @Start AND Date <= @End " +
                    "GROUP BY l.Host ORDER BY COUNT(*) DESC",
                    new { N = n, Start = start, End = end },
                    cancellationToken: token))).ToList());
        }
    }
}
