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
    public class ItemControllerTests
    {
        private readonly ItemController _controller;
        private readonly Mock<IItemRepository> mockItemRepository;

        public ItemControllerTests()
        {
            mockItemRepository = new Mock<IItemRepository>();
            mockItemRepository.Setup(m => m.Read(1)).ReturnsAsync(new ItemDetailsDTO());
            mockItemRepository.Setup(m => m.ReadFromContainer(1)).Returns(new List<ItemDetailsDTO>().AsQueryable());
            mockItemRepository.Setup(m => m.Delete(3)).ReturnsAsync(HttpStatusCode.NotFound);
            mockItemRepository.Setup(m => m.Delete(1)).ReturnsAsync(HttpStatusCode.OK);
            _controller = new ItemController(mockItemRepository.Object);
        }
    }
}