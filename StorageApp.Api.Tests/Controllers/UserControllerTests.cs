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
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> mockUserRepository;

        public UserControllerTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.Read(1)).ReturnsAsync(new UserDetailsDTO());
            mockUserRepository.Setup(m => m.Delete(2)).ReturnsAsync(HttpStatusCode.NotFound);
            mockUserRepository.Setup(m => m.Delete(1)).ReturnsAsync(HttpStatusCode.OK);
            _controller = new UserController(mockUserRepository.Object);
        }
    }
}