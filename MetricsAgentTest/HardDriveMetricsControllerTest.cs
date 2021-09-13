using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;

namespace MetricsAgentTest
{
    public class HardDriveMetricsControllerTest
    {
        HardDriveMetricsController _controller;
        Mock<ILogger<HardDriveMetricsController>> _mockLogger;
        Mock<IRepository<HardDriveMetric>> _mockRepository;

        public HardDriveMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<HardDriveMetricsController>>();
            _mockRepository = new Mock<IRepository<HardDriveMetric>>();

            _controller = new HardDriveMetricsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<HardDriveMetric>())).Verifiable();

            var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<HardDriveMetric>()), Times.AtMostOnce());
        }
    }
}
