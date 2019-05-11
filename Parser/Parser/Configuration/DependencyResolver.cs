using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parser.BLL;
using Parser.Data;
using Parser.Data.Core.DataAccess;
using Parser.Data.DataAccess;
using System;

namespace Parser.Configuration
{
    class DependencyResolver
    {
        public IServiceProvider ServiceProvider { get; }

        public DependencyResolver()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigurationService, ConfigurationService>();
            
            services.
                AddTransient<IParser, BLL.Parser>().
                AddTransient<ILineParser, AccessLogLineParser>(x => new AccessLogLineParser(
                    new[] { ".jpg", ".gif", ".png", ".css", ".js" })).
                AddTransient<ILogService, LogService>();

            services.AddScoped<IAsyncRepository, AsyncRepository>();

            // Register DbContext class
            services.AddScoped(provider =>
            {
                var configService = provider.GetService<IConfigurationService>();
                var connectionString = configService.GetConfiguration().GetConnectionString("Default");
                var optionsBuilder = new DbContextOptionsBuilder<ParserDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new ParserDbContext(optionsBuilder.Options);
            });
        }
    }
}
