using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Parser.Data;
using System.IO;
using System.Reflection;

namespace Parser
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ParserDbContext>
    {
        public ParserDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
            var builder = new DbContextOptionsBuilder<ParserDbContext>();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseSqlServer(connectionString);
            return new ParserDbContext(builder.Options);
        }
    }
}
