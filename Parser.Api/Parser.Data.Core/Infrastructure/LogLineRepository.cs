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
    public class LogLineRepository : AsyncRepository
    {
        public LogLineRepository(IParserConnection connection) : base(connection)
        {
        }
        public Task<List<LogLine>> GetTop(CancellationToken token = default(CancellationToken))
        {
            return this.RunAsync(async (dbConnection) =>
              (await dbConnection.QueryAsync<LogLine>
                (new CommandDefinition(
                    "SELECT TOP (@N) l.Host FROM LogLines l WHERE Date BETWEEN @Start AND @End " +
                    "GROUP BY l.Host ORDER BY COUNT(*) DESC",
                    new { N=10, Start= DateTime.Now, End = DateTime.Now },
                    cancellationToken: token))).ToList());
        }
    }
}
