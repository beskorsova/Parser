using Microsoft.EntityFrameworkCore.Design;
using Parser.Configuration;
using Parser.Data;

namespace Parser
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ParserDbContext>
    {
        public ParserDbContext CreateDbContext(string[] args)
        {
            var resolver = new DependencyResolver();
            return resolver.ServiceProvider.GetService(typeof(ParserDbContext)) as ParserDbContext;

        }
    }
}
