using Microsoft.EntityFrameworkCore;

namespace Parser.Data
{
    public class ParserDbContext : DbContext
    {
        protected ParserDbContext(DbContextOptions<ParserDbContext> options) : base(options)
        {
        }
    }
}
