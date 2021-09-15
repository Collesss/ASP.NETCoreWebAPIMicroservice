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
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    public class NetMetricsController : BaseMetricsController<NetMetricsController, NetMetric, NetMetricCreateRequestDto>
    {
        public NetMetricsController(ILogger<NetMetricsController> logger, IRepository<NetMetric> repository, IMapper mapper) : base(logger, repository, mapper)
        {

        }
    }
}
