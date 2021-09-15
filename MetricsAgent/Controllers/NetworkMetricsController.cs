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
    public class NetworkMetricsController : BaseMetricsController<NetworkMetricsController, NetworkMetric, NetworkMetricCreateRequestDto>
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IRepository<NetworkMetric> repository, IMapper mapper) : base(logger, repository, mapper)
        {

        }
    }
}
