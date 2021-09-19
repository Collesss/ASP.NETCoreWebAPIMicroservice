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
    public class HardDriveMetricsController : BaseMetricsController<HardDriveMetricsController, HardDriveMetricAgent>
    {
        public HardDriveMetricsController(ILogger<HardDriveMetricsController> logger, IRepositoryMetricAgents<HardDriveMetricAgent> repository) : base(logger, repository) { }
    }
}
