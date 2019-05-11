using Microsoft.Extensions.Configuration;

namespace Parser.Configuration
{
    interface IConfigurationService
    {
        IConfigurationRoot GetConfiguration();
    }
}
