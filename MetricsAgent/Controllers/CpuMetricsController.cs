using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    public class CpuMetricsController : BaseMetricsController<CpuMetricsController, CpuMetric>
    {
        public CpuMetricsController(ILogger<CpuMetricsController> logger, IRepository<CpuMetric> repository) : base(logger, repository)
        {
            
        }
    }
}
