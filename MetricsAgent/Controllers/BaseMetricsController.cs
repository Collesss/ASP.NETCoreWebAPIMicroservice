using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseMetricsController<T, V> : ControllerBase where T : BaseMetricsController<T, V> where V : BaseMetric, new()
    {
        protected readonly ILogger<T> _logger;
        protected readonly IRepository<V> _repository;
        protected BaseMetricsController(ILogger<T> logger, IRepository<V> repository)
        {
            _logger = logger;
            _repository = repository;
            _logger.LogDebug(1, $"NLog встроен в {GetType().Name}");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public virtual IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"параметры метода (GetMetricsFromAgent)| {nameof(fromTime),8}: {fromTime,12}; {nameof(toTime),8}: {toTime,12};");
            return Ok(_repository.GetAll().Where(metric => metric.Time >= fromTime && metric.Time < toTime).OrderBy(metric => metric.Time));
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            _logger.LogInformation("вызов метода (GetAll)");
            return Ok(_repository.GetAll().OrderBy(metric => metric.Time));
        }

        [HttpPost]
        public virtual IActionResult Post([FromBody] MetricCreateResponse metric)
        {
            _repository.Create(new V() { Value = metric.Value, Time = TimeSpan.FromSeconds(metric.Time) });
            return Ok();
        }

        [HttpPut]
        public virtual IActionResult Put([FromBody] V metric)
        {
            _repository.Update(metric);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete([FromRoute]int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
