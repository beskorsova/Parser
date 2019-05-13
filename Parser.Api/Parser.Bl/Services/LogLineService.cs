using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Parser.Core.Interfaces;
using Parser.Core.Models;
using Parser.Data.Core.DataAccess;

namespace Parser.Bl.Services
{
    public class LogLineService: ILogLineService
    {
        private readonly ILogLineRepository logLineRepository;
        private readonly IMapper mapper;

        public LogLineService(ILogLineRepository logLineRepository, IMapper mapper)
        {
            this.logLineRepository = logLineRepository;
            this.mapper = mapper;
        }

        public async Task<List<LogLineDataModel>> GetAll(DateTime? start, DateTime? end, int offset, int limit = 10, CancellationToken token = default(CancellationToken))
        {
            var logLines = await this.logLineRepository.GetAll(start, end, offset, limit, token);
            return this.mapper.Map<List<LogLineDataModel>>(logLines);
        }

        public async Task<List<string>> GetTopHosts(TopFilterDataModel filter, CancellationToken token = default(CancellationToken))
        {
            return (await this.logLineRepository.GetTopHosts(filter.N, filter.Start, filter.End, token))
                .Select(x => x.Host).ToList();
        }

        public async Task<List<string>> GetTopRoutes(TopFilterDataModel filter, CancellationToken token = default(CancellationToken))
        {
            return (await this.logLineRepository.GetTopRoutes(filter.N, filter.Start, filter.End, token))
               .Select(x => x.Route).ToList();
        }
    }
}
