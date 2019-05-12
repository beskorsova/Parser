using Parser.BLL.Mappings;
using Parser.BLL.Models;
using Parser.BLL.Services.Interfaces;
using Parser.Data.Core.DataAccess;
using System.Collections.Generic;
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
        public async Task CreateAsync(IEnumerable<Task<LogLineModel>> logLinesModel)
        {
            var logLines = logLinesModel.ToLogLines();
            foreach (var logLine in logLines)
            {
                await this.repository.AddAsync(logLine);
            }
            await this.repository.SaveChangesAsync();
        }

        public async Task CreateAsync(List<LogLineModel> logLinesModel)
        {
            var logLines = logLinesModel.ToLogLines();
            foreach (var logLine in logLines)
            {
                await this.repository.AddAsync(logLine);
            }
            await this.repository.SaveChangesAsync();
        }

        public void Create(IEnumerable<Task<LogLineModel>> logLinesModel)
        {
            var logLines = logLinesModel.ToLogLines();
            foreach (var logLine in logLines)
            {
                this.repository.Add(logLine);
            }
            this.repository.SaveChanges();
        }

        public void Create(List<LogLineModel> logLinesModel)
        {
            var logLines = logLinesModel.ToLogLines();
            foreach (var logLine in logLines)
            {
                this.repository.Add(logLine);
            }
            this.repository.SaveChanges();
        }

        public void Create(LogLineModel logLineModel)
        {
            var logLine = logLineModel.ToLogLine();
            //foreach(var logLine in logLines)
            //{
            this.repository.Add(logLine);
            // }
           //  this.repository.SaveChanges();
        }

        public void SaveChanges()
        {
            this.repository.SaveChanges();
        }
    }
}
