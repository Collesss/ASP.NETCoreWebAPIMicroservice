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

        [HttpPost]
        public IActionResult Create([FromBody]MetricAgentCreateRequestDto CreateRequestDto)
        {
            var res = _dbContext.MetricAgents.Add(_mapper.Map<MetricAgent>(CreateRequestDto));

            var c = _dbContext.SaveChanges();

            return Ok(res.Entity);
        }

        [HttpPut]
        public IActionResult Update([FromBody] MetricAgentUpdateRequestDto CreateRequestDto)
        {
            var res = _dbContext.MetricAgents.Update(_mapper.Map<MetricAgent>(CreateRequestDto));

            

            var c = _dbContext.SaveChanges();

            return Ok(res.Entity);
        }
    }
}
