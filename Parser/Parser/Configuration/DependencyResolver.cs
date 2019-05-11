using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Parser.BLL.Options;
using Parser.BLL.Parse;
using Parser.BLL.Parse.Interfaces;
using Parser.BLL.Services;
using Parser.BLL.Services.Interfaces;
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
            
            services.Configure<ExcludeRule>(x =>
            {
                x.Routes = new string[] { ".jpg", ".gif", ".png", ".css", ".js" };
            });

            services.
                AddTransient<IParser, BLL.Parse.Parser>().
                AddTransient<ILineParser, AccessLogLineParser>(x =>
                new AccessLogLineParser(x.GetService<IOptions<ExcludeRule>>().Value)).
                AddTransient<ILogService, LogService>();

            services.AddScoped<IAsyncRepository, AsyncRepository>();

            services.AddTransient<IParserStoreService, ParserStoreService>();

            // Register ParserDbContext
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
