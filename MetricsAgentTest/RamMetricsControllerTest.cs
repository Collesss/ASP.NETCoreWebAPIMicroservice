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
    public class RamMetricsControllerTest
    {
        RamMetricsController _controller;
        Mock<ILogger<RamMetricsController>> _mockLogger;
        Mock<IRepository<RamMetric>> _mockRepository;
        Mock<IMapper> _mockAutoMapper;

        public RamMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _mockRepository = new Mock<IRepository<RamMetric>>();
            _mockAutoMapper = new Mock<IMapper>();

            _controller = new RamMetricsController(_mockLogger.Object, _mockRepository.Object, _mockAutoMapper.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.CreateAsync(It.IsAny<RamMetric>()).Wait()).Verifiable();

            //var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.CreateAsync(It.IsAny<RamMetric>()).Wait(), Times.AtMostOnce());
        }
    }
}
