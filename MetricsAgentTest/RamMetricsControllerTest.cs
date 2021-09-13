using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;

namespace MetricsAgentTest
{
    public class RamMetricsControllerTest
    {
        RamMetricsController _controller;
        Mock<ILogger<RamMetricsController>> _mockLogger;
        Mock<IRepository<RamMetric>> _mockRepository;

        public RamMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _mockRepository = new Mock<IRepository<RamMetric>>();

            _controller = new RamMetricsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}
