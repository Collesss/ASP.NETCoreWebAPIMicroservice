using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1TestEF.Models;
using WebApplication1TestEF.Models.DbContextAndRepository;
using WebApplication1TestEF.Models.Dto;

namespace WebApplication1TestEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestCRUDController : ControllerBase
    {
        private WebApplication1TestEFDbContext _dbContext;
        private IMapper _mapper;

        public TestCRUDController(WebApplication1TestEFDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.MetricAgents.AsQueryable().ToList());
        }

        /*
        [HttpPost]
        public IActionResult Create([FromBody] MetricAgentCreateOrUpdateRequestDto CreateRequestDto)
        {
            var res = _dbContext.MetricAgents.Add(_mapper.Map<MetricAgent>(CreateRequestDto));

            var c = _dbContext.SaveChanges();

            return Ok(res.Entity);
        }

        [HttpPut]
        public IActionResult Update([FromBody] MetricAgentUpdateRequestDto CreateRequestDto)
        {
            var res = _dbContext.MetricAgents.Update(_mapper.Map<MetricAgent>(CreateRequestDto));

            //_dbContext.MetricAgents.Attach

            var c = _dbContext.SaveChanges();

            return Ok(res.Entity);
        }
        */

        [HttpPost("RegOrUpd")]
        public IActionResult CreateOrUpdate([FromBody] MetricAgentCreateOrUpdateRequestDto createRequestDto)
        {
            MetricAgent metricAgent = _mapper.Map<MetricAgentCreateOrUpdateRequestDto, MetricAgent>(createRequestDto, opts => opts.AfterMap((source, dest) => {
                dest.Id = _dbContext.MetricAgents.SingleOrDefault(agent => agent.AddressAgent == source.AddressAgent)?.Id ?? 0;
                dest.LastUpdateTime = DateTime.Now;
            }));
            
            var res = _dbContext.MetricAgents.Update(metricAgent);
            _dbContext.SaveChanges();

            return Ok(_mapper.Map<MetricAgentCreateOrUpdateResponseDto>(res));
        }
    }
}
