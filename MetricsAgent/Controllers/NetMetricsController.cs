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
    public class NetMetricsController : BaseMetricsController<NetMetricsController, NetMetric, NetMetricCreateRequestDto>
    {
        public NetMetricsController(ILogger<NetMetricsController> logger, IRepository<NetMetric> repository, IMapper mapper) : base(logger, repository, mapper)
        {

        }

        /// <summary>
        /// Получает метрики Net на заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная метка времени</param>
        /// <param name="toTime">конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        public override async Task<IActionResult> GetMetricsFromAgent([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime) =>
            await base.GetMetricsFromAgent(fromTime, toTime);

        /// <summary>
        /// Получает метрики Net сохранённые за всё время
        /// </summary>
        /// <returns>Список метрик, которые были сохранены</returns>
        public override async Task<IActionResult> GetAllMetricsFromAgent() =>
            await base.GetAllMetricsFromAgent();
    }
}
