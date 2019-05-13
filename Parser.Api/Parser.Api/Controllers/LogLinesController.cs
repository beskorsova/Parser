using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parser.Api.Model.Filters;
using Parser.Data.Core.Entities;
using Parser.Data.Core.Infrastructure;

namespace Parser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogLinesController : ControllerBase
    {
        private readonly LogLineRepository logLineRepository;
        public LogLinesController(LogLineRepository logLineRepository)
        {
            this.logLineRepository = logLineRepository;
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<List<LogLine>>> GetAsync()
        {
            return await this.logLineRepository.GetTop();
        }
        
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        
    }
}
