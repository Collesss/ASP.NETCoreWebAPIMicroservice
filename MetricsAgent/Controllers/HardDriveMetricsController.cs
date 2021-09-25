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

        /// <summary>
        /// Получает метрики HardDrive на заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная метка времени</param>
        /// <param name="toTime">конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        public override async Task<IActionResult> GetMetricsFromAgent([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime) =>
            await base.GetMetricsFromAgent(fromTime, toTime);

        /// <summary>
        /// Получает метрики HardDrive сохранённые за всё время
        /// </summary>
        /// <returns>Список метрик, которые были сохранены</returns>
        public override async Task<IActionResult> GetAllMetricsFromAgent() =>
            await base.GetAllMetricsFromAgent();
    }
}
