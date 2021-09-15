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
    public class CpuMetricsControllerTest
    {
        CpuMetricsController _controller;
        Mock<ILogger<CpuMetricsController>> _mockLogger;
        Mock<IRepository<CpuMetric>> _mockRepository;
        Mock<IMapper> _mockAutoMapper;


        public CpuMetricsControllerTest()
        {
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockRepository = new Mock<IRepository<CpuMetric>>();
            _mockAutoMapper = new Mock<IMapper>();

            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepository.Object, _mockAutoMapper.Object);
        }

        [Fact]
        public void GetMetricsFromAgentTest()
        {
            _mockRepository.Setup(repository => repository.CreateAsync(It.IsAny<CpuMetric>()).Wait()).Verifiable();

            /*
            var fromTime = new TimeSpan(1, 2, 3, 4);
            var toTime = new TimeSpan(10, 20, 30, 40);

            var result = _controller.GetMetricsFromAgent(fromTime, toTime);
            */

            //var result = _controller.Post(new MetricCreateResponse { Value = 50, Time = TimeSpan.FromSeconds(1) });

            _mockRepository.Verify(repository => repository.CreateAsync(It.IsAny<CpuMetric>()).Wait(), Times.AtMostOnce());

            //Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
