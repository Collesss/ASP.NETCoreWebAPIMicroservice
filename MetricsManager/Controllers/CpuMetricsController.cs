using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    public class CpuMetricsController : BaseMetricsController<CpuMetricsController>
    {
        public CpuMetricsController(ILogger<CpuMetricsController> logger) : base(logger) { }
    }
}
