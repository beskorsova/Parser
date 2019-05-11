using Microsoft.EntityFrameworkCore;
using Parser.Data.Core.Entities;

namespace Parser.Data
{
    public class ParserDbContext : DbContext
    {
        public ParserDbContext(DbContextOptions<ParserDbContext> options) : base(options)
        {
        }

        public DbSet<QueryParameter> QueryParameters { get; set; }
        public DbSet<LogLine> LogLines { get; set; }
    }
}
