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
    public class NetMetricsControllerTest
    {
        NetMetricsController _controller;
        Mock<ILogger<NetMetricsController>> _mockLogger;
        Mock<IRepository<NetMetric>> _mockRepository;
        Mock<IMapper> _mockAutoMapper;

        public NetMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<NetMetricsController>>();
            _mockRepository = new Mock<IRepository<NetMetric>>();
            _mockAutoMapper = new Mock<IMapper>();

            _controller = new NetMetricsController(_mockLogger.Object, _mockRepository.Object, _mockAutoMapper.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.CreateAsync(It.IsAny<NetMetric>()).Wait()).Verifiable();

            //var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.CreateAsync(It.IsAny<NetMetric>()).Wait(), Times.AtMostOnce());
        }
    }
}
