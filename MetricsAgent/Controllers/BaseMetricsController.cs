using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public virtual IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
