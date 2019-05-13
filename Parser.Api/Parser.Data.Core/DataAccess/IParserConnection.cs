using System.Data;

namespace Parser.Data.Core.DataAccess
{
    public interface IParserConnection
    {
        IDbConnection Open();
    }
}
