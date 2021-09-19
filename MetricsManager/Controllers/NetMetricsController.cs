using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMetricsManager;
using EntitiesMetricsManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    public class NetMetricsController : BaseMetricsController<NetMetricsController, NetMetricAgent>
    {
        public NetMetricsController(ILogger<NetMetricsController> logger, IRepositoryMetricAgents<NetMetricAgent> repository) : base(logger, repository) { }
    }
}
