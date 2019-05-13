using Parser.Data.Core.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace Parser.Data.Core.Infrastructure
{
    public class ParserConnection : IParserConnection
    {
        private readonly string connectionString;
        public ParserConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Open()
        {
            var connection = new SqlConnection(this.connectionString);
            return connection;
        }
    }
}
