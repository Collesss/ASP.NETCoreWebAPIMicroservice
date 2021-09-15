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
    public class HardDriveMetricsControllerTest
    {
        HardDriveMetricsController _controller;
        Mock<ILogger<HardDriveMetricsController>> _mockLogger;
        Mock<IRepository<HardDriveMetric>> _mockRepository;
        Mock<IMapper> _mockAutoMapper;

        public HardDriveMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<HardDriveMetricsController>>();
            _mockRepository = new Mock<IRepository<HardDriveMetric>>();
            _mockAutoMapper = new Mock<IMapper>();

            _controller = new HardDriveMetricsController(_mockLogger.Object, _mockRepository.Object, _mockAutoMapper.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.CreateAsync(It.IsAny<HardDriveMetric>()).Wait()).Verifiable();

            //var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.CreateAsync(It.IsAny<HardDriveMetric>()).Wait(), Times.AtMostOnce());
        }
    }
}
