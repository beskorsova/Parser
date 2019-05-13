using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Parser.Api.Model.Filters;
using Parser.Data.Core.DataAccess;
using Parser.Data.Core.Entities;

namespace Parser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogLinesController : ControllerBase
    {
        private readonly ILogLineRepository logLineRepository;
        public LogLinesController(ILogLineRepository logLineRepository)
        {
            this.logLineRepository = logLineRepository;
        }

        [HttpGet("topHosts")]
        public async Task<ActionResult<List<string>>> GetTopHostsAsync([FromQuery] TopFilterModel filterModel)
        {
            return (await this.logLineRepository.GetTopHosts(filterModel.N, filterModel.Start.Value, filterModel.End.Value))
                .Select(x=>x.Host).ToList();
        }

        [HttpGet("topRoutes")]
        public async Task<ActionResult<List<string>>> GetTopRoutesAsync([FromQuery] TopFilterModel filterModel)
        {
            return (await this.logLineRepository.GetTopRoutes(filterModel.N, filterModel.Start.Value, filterModel.End.Value))
                 .Select(x => x.Host).ToList();
        }

        [HttpGet]
        public async Task<ActionResult<List<LogLine>>> GetAll([FromQuery] TopFilterModel filterModel)
        {
            return await this.logLineRepository.GetAll(filterModel.Start.Value, filterModel.End.Value, 0);
        }
        
    }
}
