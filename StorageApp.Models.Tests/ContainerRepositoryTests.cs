using System;
using Xunit;
using StorageApp.Models;
using StorageApp.Entities;
using StorageApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using StorageApp.Models.Test;
using static System.Net.HttpStatusCode;

namespace StorageApp.Models.Tests
{
    public class ContainerRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly StorageContext _context;
        private readonly IContainerRepository _repository;

        public ContainerRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<StorageContext>().UseSqlite(_connection);

            _context = new StorageContext(builder.Options);
            _context.Database.EnsureCreated();
            _context.GenerateTestData();

            _repository = new ContainerRepository(_context);
        }

        [Fact]
        public async Task Create_container_returns_2_change_and_id_2()
        {   
            // Arrange
            var container = new ContainerCreateDTO {
                Name = "godcontainer",
                UserId = 1,
            };

            // Act
            var result = await _repository.Create(container);

            // Assert
            Assert.Equal(2, result.affectedRows);
            Assert.Equal(2, result.id); // Id of new user will be 2 since one user is already made with id=1 through GenerateTestData()
        }

        [Fact]
        public async Task Read_non_existing_container_returns_null()
        {
            // Arrange
            var containerId = 2;

            // Act
            var readContainer = await _repository.Read(containerId);

            // Assert
            Assert.Null(readContainer);
        }

        [Fact]
        public async Task Read_existing_container_returns_correct_container_from_test_data()
        {
            // Arrange
            var containerId = 1;

            // Act
            var readContainer = await _repository.Read(containerId);

            // Assert
            Assert.Equal("Fryser", readContainer.Name);
            Assert.Equal(1, readContainer.Items.Count);
            Assert.Equal(1, readContainer.Users.Count);
        }

        [Fact]
        public async Task Read_all_containers_returns_correct_list()
        {
            // Act
            var allContainers = await _repository.ReadFromUser(1).ToListAsync();

            // Assert
            Assert.Equal(1, allContainers.Count);
            Assert.Equal("Fryser", allContainers[0].Name);
        }

        [Fact]
        public async Task Update_non_existing_container_returns_notfound()
        {
            // Arrange
            var containerUpdate = new ContainerUpdateDTO {
                Id = 2,
                Name = "køleskab"
            };

            // Act
            var result = await _repository.Update(containerUpdate);

            // Assert
            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async Task Update_existing_container_with_unassigned_item_returns_OK()
        {
            // Arrange
            var containerUpdate = new ContainerUpdateDTO {
                Id = 1,
                ItemId = 1
            };

            // Act
            var result = await _repository.Update(containerUpdate);

            // Assert
            Assert.Equal(OK, result);
        }

        [Fact]
        public async Task Update_existing_container_with_assigned_item_returns_Badrequest()
        {
            // Arrange
            var containerUpdate = new ContainerUpdateDTO {
                Id = 1,
                ItemId = 2
            };

            // Act
            var result = await _repository.Update(containerUpdate);

            // Assert
            Assert.Equal(BadRequest, result);
        }

        [Fact]
        public async Task Delete_non_existing_container_returns_notfound()
        {
            // Arrange
            var containerId = 2;

            // Act
            var result = await _repository.Delete(containerId);

            // Assert
            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async Task Delete_existing_container_returns_OK()
        {
            // Arrange
            var containerId = 1;

            // Act
            var result = await _repository.Delete(containerId);

            // Assert
            Assert.Equal(OK, result);
        }

        public void Dispose()
        {
            _connection.Dispose();
            _context.Dispose();
        }
    }
}