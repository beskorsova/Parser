using System.Threading;
using System.Threading.Tasks;

namespace Parser.Data.Core.DataAccess
{
    public interface IAsyncRepository
    {
        Task AddAsync<T>(T entity, CancellationToken token = default(CancellationToken))
            where T : BaseEntity;

        Task SaveChangesAsync();
    }
}
