using System.Collections.Generic;
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

        [HttpGet]
        public async Task<ActionResult<List<LogLine>>> GetAsync([FromQuery] TopFilterModel filterModel)
        {
            return await this.logLineRepository.GetTop(filterModel.N, filterModel.Start.Value, filterModel.End.Value);
        }
        
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        
    }
}
