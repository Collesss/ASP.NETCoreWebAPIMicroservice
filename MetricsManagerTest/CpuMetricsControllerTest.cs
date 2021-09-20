using System;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTest
{
    public class CpuMetricsControllerTest
    {
        CpuMetricsController _controller;
        Mock<ILogger<CpuMetricsController>> _mock;

        public CpuMetricsControllerTest()
        {
            _mock = new Mock<ILogger<CpuMetricsController>>();

            //_controller = new CpuMetricsController(_mock.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            var agentId = 1;
            var fromTime = new DateTime(1, 2, 3);
            var toTime = new DateTime(10, 2, 30);

            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime).Result;

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster()
        {
            var fromTime = new DateTime(1, 2, 3);
            var toTime = new DateTime(10, 2, 30);

            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
