using System;
using System.Data;
using System.Threading.Tasks;
namespace Parser.Data.Core.DataAccess
{
    public abstract class AsyncRepository
    {
        private readonly IParserConnection Connection;

        public AsyncRepository(IParserConnection connection)
        {
            this.Connection = connection;
        }
        
        protected async Task<TResult> RunAsync<TResult>(Func<IDbConnection, Task<TResult>> exec)
        {
            using (var dbConnection = this.Connection.Open())
            {
                return await exec(dbConnection);
            }
        }
    }
}
