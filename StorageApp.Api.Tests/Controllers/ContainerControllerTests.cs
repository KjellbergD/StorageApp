using System.Net;
using System.Linq;
using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using StorageApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using StorageApp.Models;
using StorageApp.Shared;

namespace StorageApp.Api.Test
{
    public class ContainerControllerTests
    {
        private readonly ContainerController _controller;
        private readonly Mock<IContainerRepository> mockContainerRepository;

        public ContainerControllerTests()
        {
            mockContainerRepository = new Mock<IContainerRepository>();
            mockContainerRepository.Setup(m => m.Read(1)).ReturnsAsync(new ContainerDetailsDTO());
            mockContainerRepository.Setup(m => m.ReadFromUser(1)).Returns(new List<ContainerListDTO>().AsQueryable());
            mockContainerRepository.Setup(m => m.Delete(3)).ReturnsAsync(HttpStatusCode.NotFound);
            mockContainerRepository.Setup(m => m.Delete(1)).ReturnsAsync(HttpStatusCode.OK);
            _controller = new ContainerController(mockContainerRepository.Object);
        }
    }
}