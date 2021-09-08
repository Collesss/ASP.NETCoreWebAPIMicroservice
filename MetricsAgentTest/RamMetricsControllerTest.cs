using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;

namespace MetricsAgentTest
{
    public class RamMetricsControllerTest
    {
        RamMetricsController _controller;

        public RamMetricsControllerTest()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            var fromTime = new TimeSpan(1, 2, 3, 4);
            var toTime = new TimeSpan(10, 20, 30, 40);

            var result = _controller.GetMetricsFromAgent(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
