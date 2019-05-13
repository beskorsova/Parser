using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parser.Api.Model;
using Parser.Api.Model.Filters;
using Parser.Core.Interfaces;
using Parser.Core.Models;

namespace Parser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogLinesController : ControllerBase
    {
        private readonly ILogLineService logLineService;
        private readonly IMapper mapper;

        public LogLinesController(ILogLineService logLineService, IMapper mapper)
        {
            this.logLineService = logLineService;
            this.mapper = mapper;
        }

        [HttpGet("topHosts")]
        public async Task<ActionResult<List<string>>> GetTopHostsAsync([FromQuery] TopFilterModel filterModel)
        {
            return await this.logLineService.GetTopHosts(filterModel.N, filterModel.Start, filterModel.End);
        }

        [HttpGet("topRoutes")]
        public async Task<ActionResult<List<string>>> GetTopRoutesAsync([FromQuery] TopFilterModel filterModel)
        {
            return await this.logLineService.GetTopRoutes(filterModel.N, filterModel.Start, filterModel.End);
        }

        [HttpGet]
        public async Task<ActionResult<List<LogLineModel>>> GetAll([FromQuery] TableFilterModel filterModel)
        {
            var logLinesData = await this.logLineService.GetAll(filterModel.Start, filterModel.End, filterModel.Offset, filterModel.Limit);
            return this.mapper.Map<List<LogLineModel>>(logLinesData);
        }
        
    }
}
