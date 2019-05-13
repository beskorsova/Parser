using Dapper;
using Parser.Data.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Data.Core.DataAccess
{
    public class LogLineRepository : AsyncRepository, ILogLineRepository
    {
        private readonly string datePeriodFilter;
        public LogLineRepository(IParserConnection connection) : base(connection)
        {
            this.datePeriodFilter = "WHERE Date >= @Start AND Date <= @End ";
        }

        private string GetDateFilter(DateTime? start, DateTime? end)
        {
            if(start == null || end == null)
            {
                return string.Empty;
            }
            return this.datePeriodFilter;
        }

        public Task<List<LogLine>> GetAll(DateTime? start, DateTime? end, int offset, int limit = 10, CancellationToken token = default(CancellationToken))
        {

            var lookup = new Dictionary<long, LogLine>();
            return this.RunAsync(async (dbConnection) =>
               (await dbConnection.QueryAsync<LogLine, QueryParameter, LogLine>
                 (new CommandDefinition(
                     "SELECT l.*, qp.* FROM LogLines l inner join QueryParameters qp on l.Id = qp.LogLineId " +
                     this.GetDateFilter(start, end) +
                     "ORDER BY l.Date ASC " +
                     "OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY",
                      new
                      {
                          Offset = offset,
                          Limit = limit,
                          Start = start ?? default(DateTime),
                          End = end ?? default(DateTime),
                      }, cancellationToken: token),
                      (l, qp) => {
                          LogLine logLine;
                          if (!lookup.TryGetValue(l.Id, out logLine))
                          {
                              lookup.Add(l.Id, logLine = l);
                          }
                          logLine.Parameters.Add(qp);
                          return logLine;
                      })).ToList());
        }

        public Task<List<LogLine>> GetTopHosts(int n, DateTime? start, DateTime? end,
            CancellationToken token = default(CancellationToken))
        {
            return this.RunAsync(async (dbConnection) =>
              (await dbConnection.QueryAsync<LogLine>
                (new CommandDefinition(
                    "SELECT TOP (@N) l.Host FROM LogLines l " +
                    this.GetDateFilter(start, end) +
                    "GROUP BY l.Host ORDER BY COUNT(*) DESC",
                    new { N = n, Start = start ?? default(DateTime), End = end ?? default(DateTime) },
                    cancellationToken: token))).ToList());
        }

        public Task<List<LogLine>> GetTopRoutes(int n, DateTime? start, DateTime? end, CancellationToken token = default(CancellationToken))
        {
            return this.RunAsync(async (dbConnection) =>
              (await dbConnection.QueryAsync<LogLine>
                (new CommandDefinition(
                    "SELECT TOP (@N) l.Route FROM LogLines l " +
                    this.GetDateFilter(start, end) +
                    "GROUP BY l.Route ORDER BY COUNT(*) DESC",
                    new { N = n, Start = start ?? default(DateTime), End = end ?? default(DateTime) },
                    cancellationToken: token))).ToList());
        }
    }
}
