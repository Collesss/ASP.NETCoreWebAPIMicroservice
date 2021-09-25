using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DB;
using Entities;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseMetricsController<TLoggerType, VRepositoryType, NCreateRequest> : ControllerBase where TLoggerType : BaseMetricsController<TLoggerType, VRepositoryType, NCreateRequest> where VRepositoryType : BaseMetricEntity, new() where NCreateRequest : BaseMetricCreateRequestDto
    {
        protected readonly ILogger<TLoggerType> _logger;
        protected readonly IRepository<VRepositoryType> _repository;
        protected readonly IMapper _mapper;

        protected BaseMetricsController(ILogger<TLoggerType> logger, IRepository<VRepositoryType> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _logger.LogDebug(1, $"NLog встроен в {GetType().Name}");
        }

        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public virtual async Task<IActionResult> GetMetricsFromAgent([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"параметры метода (GetMetricsFromAgent)| {nameof(fromTime),8}: {fromTime,12}; {nameof(toTime),8}: {toTime,12};");
            return Ok(await _repository.GetAll().Where(metric => metric.Time >= fromTime && metric.Time < toTime).OrderBy(metric => metric.Time).ToListAsync());
        }
        
        [HttpGet]
        public virtual async Task<IActionResult> GetAllMetricsFromAgent()
        {
            _logger.LogInformation("вызов метода (GetAll)");
            return Ok(await _repository.GetAll().OrderBy(metric => metric.Time).ToListAsync());
        }

        /*
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] NCreateRequest metric)
        {
            await _repository.CreateAsync(_mapper.Map<VRepositoryType>(metric));
            return Ok();
        }
          
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] VRepositoryType metric)
        {
            await _repository.UpdateAsync(metric);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _repository.DeleteAsync(_mapper.Map<VRepositoryType>(id));
            return Ok();
        }
        */
    }
}
