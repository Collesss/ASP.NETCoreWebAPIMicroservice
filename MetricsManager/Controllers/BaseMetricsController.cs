using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseMetricsController<T> : ControllerBase where T : BaseMetricsController<T>
    {
        protected readonly ILogger<T> _logger;

        protected BaseMetricsController(ILogger<T> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType().Name}");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public virtual IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"параметры метода (GetMetricsFromAgent)| {nameof(agentId),8}: {agentId,8}; {nameof(fromTime),8}: {fromTime,12}; {nameof(toTime),8}: {toTime,12};");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public virtual IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"параметры метода (GetMetricsFromAllCluster)| {nameof(fromTime),8}: {fromTime,12}; {nameof(toTime),8}: {toTime,12};");
            return Ok();
        }
    }
}
