using System;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using Xunit;

namespace MetricsManagerTest
{
    public class HardDriveMetricsControllerTest
    {
        HardDriveMetricsController _controller;

        public HardDriveMetricsControllerTest()
        {
            _controller = new HardDriveMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            var agentId = 1;
            var fromTime = new TimeSpan(1, 2, 3, 4);
            var toTime = new TimeSpan(10, 20, 30, 40);

            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster()
        {
            var fromTime = new TimeSpan(1, 2, 3, 4);
            var toTime = new TimeSpan(10, 20, 30, 40);

            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
