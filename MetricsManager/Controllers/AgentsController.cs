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

        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            return Ok(await _repository.GetAll().OrderBy(ord => ord.Id).ToListAsync());
        }

        [HttpPost("registerOrUpdate")]
        public async Task<IActionResult> RegisterOrUpdateAgent([FromBody] AgentCreateRequestDto agentCreateDto)
        {
            MetricAgent agent = await _repository.GetAll().SingleOrDefaultAsync(agent => agent.AddressAgent == agentCreateDto.AddressAgent);
            if (agent is null)
                await _repository.CreateAsync(_mapper.Map<MetricAgent>(agentCreateDto));
            else
            {
                agent.LastUpdateTime = DateTime.Now;
                await _repository.UpdateAsync(agent);
            }
            return Ok();
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
