using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Parser.Configuration
{
    class ConfigurationService : IConfigurationService
    {
        public IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddUserSecrets(Assembly.GetExecutingAssembly())
               .Build();
        }
    }
}
