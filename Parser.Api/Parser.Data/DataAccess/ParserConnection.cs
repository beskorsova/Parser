using System.Data;
using System.Data.SqlClient;

namespace Parser.Data.Core.DataAccess
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
