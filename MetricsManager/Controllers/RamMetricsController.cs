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
    public class RamMetricsController : BaseMetricsController<RamMetricsController, RamMetricAgent>
    {
        public RamMetricsController(ILogger<RamMetricsController> logger, IRepositoryMetricAgents<RamMetricAgent> repository) : base(logger, repository) { }
    }
}
