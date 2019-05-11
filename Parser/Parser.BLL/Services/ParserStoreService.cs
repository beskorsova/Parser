using Parser.BLL.DTO;
using Parser.BLL.Mappings;
using Parser.BLL.Services.Interfaces;
using Parser.Data.Core.DataAccess;
using System.Threading.Tasks;

namespace Parser.BLL.Services
{
    public class ParserStoreService: IParserStoreService
    {
        private readonly IAsyncRepository repository;

        public ParserStoreService(IAsyncRepository repository)
        {
            this.repository = repository;
        }
        public async Task CreateAsync(LogLineDto logLineDto)
        {
            var logLine = logLineDto.ToLogLine();
            await this.repository.AddAsync(logLine);
        }
    }
}
