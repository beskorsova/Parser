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

        public IConfiguration Configuration { get; protected set; }

        public DependencyResolver()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationService().GetConfiguration();
            
            services.Configure<ExcludeRuleOptions>(Configuration.GetSection("ExcludeRule"));
            services.Configure<GeolocationOptions>(Configuration.GetSection("Geolocation"));

            var connectionString = Configuration.GetConnectionString("Default");
            services.
                AddDbContext<ParserDbContext>(options =>
                options.UseSqlServer(connectionString)).
                AddTransient<IParser, BLL.Parse.Parser>().
                AddTransient<ILogLineParserHelper, LogLineParserHelper>(provider => 
                    new LogLineParserHelper(provider.GetService<IOptions<GeolocationOptions>>().Value)
                ).
                AddTransient<LineParserBase, AccessLogLineParser>(provider =>
                    new AccessLogLineParser(provider.GetService<ILogLineParserHelper>()
                    ,provider.GetService<IOptions<ExcludeRuleOptions>>().Value)).
                AddTransient<ILogService, LogService>();

            services.AddScoped<IAsyncRepository, AsyncRepository>();

            services.AddTransient<IParserStoreService, ParserStoreService>();
        }
    }
}
