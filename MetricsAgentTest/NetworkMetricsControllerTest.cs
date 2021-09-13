using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;

namespace MetricsAgentTest
{
    public class NetworkMetricsControllerTest
    {
        NetworkMetricsController _controller;
        Mock<ILogger<NetworkMetricsController>> _mockLogger;
        Mock<IRepository<NetworkMetric>> _mockRepository;

        public NetworkMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockRepository = new Mock<IRepository<NetworkMetric>>();

            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
    }
}
