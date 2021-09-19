using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMetricsManager;
using EntitiesMetricsManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseMetricsController<T, TEntity> : ControllerBase where T : BaseMetricsController<T, TEntity> where TEntity : BaseMetricAgent
    {
        protected readonly ILogger<T> _logger;
        protected readonly IRepositoryMetricAgents<TEntity> _repository;

        protected BaseMetricsController(ILogger<T> logger, IRepositoryMetricAgents<TEntity> repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType().Name}");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public virtual async Task<IActionResult> GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"параметры метода (GetMetricsFromAgent)| {nameof(agentId),8}: {agentId,8}; {nameof(fromTime),8}: {fromTime,12}; {nameof(toTime),8}: {toTime,12};");
            
            return Ok(await _repository.GetMetricFromAgent(agentId, fromTime, toTime));
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public virtual async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"параметры метода (GetMetricsFromAllCluster)| {nameof(fromTime),8}: {fromTime,12}; {nameof(toTime),8}: {toTime,12};");
            
            return Ok(await _repository.GetMetricFromAgents(fromTime, toTime));
        }
    }
}
