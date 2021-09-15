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
    public class RamMetricsController : BaseMetricsController<RamMetricsController, RamMetric, RamMetricCreateRequestDto>
    {
        public RamMetricsController(ILogger<RamMetricsController> logger, IRepository<RamMetric> repository, IMapper mapper) : base(logger, repository, mapper)
        {

        }
    }
}
