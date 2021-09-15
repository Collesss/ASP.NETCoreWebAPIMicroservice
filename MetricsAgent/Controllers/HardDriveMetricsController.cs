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
    public class HardDriveMetricsController : BaseMetricsController<HardDriveMetricsController, HardDriveMetric, HardDriveMetricCreateRequestDto>
    {
        public HardDriveMetricsController(ILogger<HardDriveMetricsController> logger, IRepository<HardDriveMetric> repository, IMapper mapper) : base(logger, repository, mapper)
        {

        }
    }
}
