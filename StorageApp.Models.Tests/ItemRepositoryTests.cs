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
    public class ItemRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly StorageContext _context;
        private readonly IItemRepository _repository;

        public ItemRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<StorageContext>().UseSqlite(_connection);

            _context = new StorageContext(builder.Options);
            _context.Database.EnsureCreated();
            _context.GenerateTestData();

            _repository = new ItemRepository(_context);
        }

        [Fact]
        public async Task Create_item_returns_1_change_and_id_3()
        {   
            // Arrange
            var item = new ItemCreateDTO {
                Name = "ost",
                Note = "for gammel"
            };

            // Act
            var result = await _repository.Create(item);

            // Assert
            Assert.Equal(1, result.affectedRows);
            Assert.Equal(3, result.id); // Id of new user will be 2 since one user is already made with id=1 through GenerateTestData()
        }

        [Fact]
        public async Task Read_non_existing_item_returns_null()
        {
            // Arrange
            var itemId = 3;

            // Act
            var readItem = await _repository.Read(itemId);

            // Assert
            Assert.Null(readItem);
        }

        [Fact]
        public async Task Read_existing_container_returns_correct_container_from_test_data()
        {
            // Arrange
            var itemId = 1;

            // Act
            var readItem = await _repository.Read(itemId);

            // Assert
            Assert.Equal("bær", readItem.Name);
            Assert.Equal("4poser", readItem.Note);
        }

        [Fact]
        public async Task Update_non_existing_item_returns_notfound()
        {
            // Arrange
            var itemUpdate = new ItemUpdateDTO {
                Id = 3,
                Note = "6poser"
            };

            // Act
            var result = await _repository.Update(itemUpdate);

            // Assert
            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async Task Update_existing_item_returns_OK()
        {
            // Arrange
            var itemUpdate = new ItemUpdateDTO {
                Id = 2,
                Note = "6poser"
            };

            // Act
            var result = await _repository.Update(itemUpdate);

            // Assert
            Assert.Equal(OK, result);
        }

        [Fact]
        public async Task Delete_non_existing_item_returns_notfound()
        {
            // Arrange
            var itemId = 3;

            // Act
            var result = await _repository.Delete(itemId);

            // Assert
            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async Task Delete_existing_item_returns_OK()
        {
            // Arrange
            var itemId = 1;

            // Act
            var result = await _repository.Delete(itemId);

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