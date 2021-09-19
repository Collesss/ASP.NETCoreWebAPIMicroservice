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
    public class NetworkMetricsController : BaseMetricsController<NetworkMetricsController, NetworkMetricAgent>
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IRepositoryMetricAgents<NetworkMetricAgent> repository) : base(logger, repository) { }
    }
}
