using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;

namespace MetricsAgentTest
{
    public class CpuMetricsControllerTest
    {
        CpuMetricsController _controller;
        Mock<ILogger<CpuMetricsController>> _mockLogger;
        Mock<IRepository<CpuMetric>> _mockRepository;


        public CpuMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockRepository = new Mock<IRepository<CpuMetric>>();

            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            /*
            var fromTime = new TimeSpan(1, 2, 3, 4);
            var toTime = new TimeSpan(10, 20, 30, 40);

            var result = _controller.GetMetricsFromAgent(fromTime, toTime);
            */

            var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());

            //Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
