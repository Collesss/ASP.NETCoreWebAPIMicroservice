using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    public class HardDriveMetricsController : BaseMetricsController<HardDriveMetricsController>
    {
        public HardDriveMetricsController(ILogger<HardDriveMetricsController> logger) : base(logger) { }
    }
}
