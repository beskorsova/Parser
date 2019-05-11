using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Core.DataAccess
{
    public interface IAsyncRepository<T, TKey> where T : IEntity<TKey>
    {
        Task AddAsync(T entity, CancellationToken token = default(CancellationToken));
    }
}
