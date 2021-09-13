using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;

namespace MetricsAgentTest
{
    public class NetMetricsControllerTest
    {
        NetMetricsController _controller;
        Mock<ILogger<NetMetricsController>> _mockLogger;
        Mock<IRepository<NetMetric>> _mockRepository;

        public NetMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<NetMetricsController>>();
            _mockRepository = new Mock<IRepository<NetMetric>>();

            _controller = new NetMetricsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<NetMetric>())).Verifiable();

            var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<NetMetric>()), Times.AtMostOnce());
        }
    }
}
