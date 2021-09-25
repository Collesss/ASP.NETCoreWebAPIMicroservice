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

        /// <summary>
        /// Получает метрики RAM агента с указанным идентификатором на заданном диапазоне времени
        /// </summary>
        /// <param name="agentId">идентификатор агента</param>
        /// <param name="fromTime">начальная метка времени</param>
        /// <param name="toTime">конечная метка времени</param>
        /// <returns>Список метрик, для указанного агента которые были сохранены в заданном диапазоне времени</returns>
        public override async Task<IActionResult> GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTime fromTime, [FromRoute] DateTime toTime) =>
            await base.GetMetricsFromAgent(agentId, fromTime, toTime);

        /// <summary>
        /// Получает метрики RAM всех агентов на заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная метка времени</param>
        /// <param name="toTime">конечная метка времени</param>
        /// <returns>Список метрик, для всех агентов которые были сохранены в заданном диапазоне времени</returns>
        public override async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime) =>
            await base.GetMetricsFromAllCluster(fromTime, toTime);
    }
}
