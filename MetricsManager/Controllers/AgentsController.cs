using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBMetricsManager;
using EntitiesMetricsManager;
using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private IRepository<MetricAgent> _repository;
        private IMapper _mapper;

        public AgentsController(IRepository<MetricAgent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Получает всех зарегистрированных агентов
        /// </summary>
        /// <returns>Список агентов которые были зарегистрированны.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            return Ok(await _repository.GetAll().OrderBy(ord => ord.Id).ToListAsync());
        }

        /// <summary>
        /// Регистрирует или обновляет запись о регистрации агента
        /// </summary>
        /// <param name="agentCreateDto">путь к агенту</param>
        /// <returns></returns>
        [HttpPost("RegOrUpd")]
        public async Task<IActionResult> RegisterOrUpdateAgent([FromBody] AgentCreateOrUpdateRequestDto agentCreateDto)
        {
            //HttpContext httpContext = HttpContext;
            
            /*
            MetricAgent agent = await _repository.GetAll().SingleOrDefaultAsync(agent => agent.AddressAgent == agentCreateDto.AddressAgent);
            if (agent is null)
                await _repository.CreateAsync(_mapper.Map<MetricAgent>(agentCreateDto));
            else
            {
                agent.LastUpdateTime = DateTime.Now;
                await _repository.UpdateAsync(agent);
            }
            */

            MetricAgent metricAgent = await _repository.GetAll().FirstOrDefaultAsync(agent => agent.AddressAgent == agentCreateDto.AddressAgent) ?? await _repository.CreateAsync(_mapper.Map<MetricAgent>(agentCreateDto));
            metricAgent.LastUpdateTime = DateTime.Now;

            return Ok(await _repository.UpdateAsync(metricAgent));
        }

        /*
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }
        */
    }
}
