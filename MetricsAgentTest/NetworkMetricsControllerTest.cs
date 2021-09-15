using System;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Entities;
using DB;
using AutoMapper;

namespace MetricsAgentTest
{
    public class NetworkMetricsControllerTest
    {
        NetworkMetricsController _controller;
        Mock<ILogger<NetworkMetricsController>> _mockLogger;
        Mock<IRepository<NetworkMetric>> _mockRepository;
        Mock<IMapper> _mockAutoMapper;

        public NetworkMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockRepository = new Mock<IRepository<NetworkMetric>>();
            _mockAutoMapper = new Mock<IMapper>();

            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepository.Object, _mockAutoMapper.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.CreateAsync(It.IsAny<NetworkMetric>()).Wait()).Verifiable();

            //var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.CreateAsync(It.IsAny<NetworkMetric>()).Wait(), Times.AtMostOnce());
        }
    }
}
