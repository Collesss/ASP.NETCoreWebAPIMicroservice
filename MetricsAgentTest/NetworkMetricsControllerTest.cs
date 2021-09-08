using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;

namespace MetricsAgentTest
{
    public class NetworkMetricsControllerTest
    {
        NetworkMetricsController _controller;

        public NetworkMetricsControllerTest()
        {
            _controller = new NetworkMetricsController();
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
