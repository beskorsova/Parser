using System;
using System.Threading;
using System.Threading.Tasks;
using Parser.Data.Core;
using Parser.Data.Core.DataAccess;

namespace Parser.Data.DataAccess
{
    public class AsyncRepository : IAsyncRepository
    {
        protected readonly ParserDbContext Context;

        public AsyncRepository(ParserDbContext context)
        {
            Context = context;
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            Context.Set<T>().Add(entity);
        }

        public async Task AddAsync<T>(T entity, CancellationToken token = default(CancellationToken)) where T : BaseEntity
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this.Context.SaveChangesAsync();
        }
    }
}
