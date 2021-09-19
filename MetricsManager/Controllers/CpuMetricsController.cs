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
    public class CpuMetricsController : BaseMetricsController<CpuMetricsController, CpuMetricAgent>
    {
        public CpuMetricsController(ILogger<CpuMetricsController> logger, IRepositoryMetricAgents<CpuMetricAgent> repository) : base(logger, repository) { }
    }
}
