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
    public class UserRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly StorageContext _context;
        private readonly IUserRepository _repository;

        public UserRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<StorageContext>().UseSqlite(_connection);

            _context = new StorageContext(builder.Options);
            _context.Database.EnsureCreated();
            _context.GenerateTestData();

            _repository = new UserRepository(_context);
        }

        [Fact]
        public async Task Create_User_returns_1_change_and_id_2()
        {   
            // Arrange
            var user = new UserCreateDTO {
                UserName = "betterdaniel",
                Password = "godtpw"
            };

            // Act
            var result = await _repository.Create(user);

            // Assert
            Assert.Equal(1, result.affectedRows);
            Assert.Equal(2, result.id); // Id of new user will be 2 since one user is already made with id=1 through GenerateTestData()
        }

        [Fact]
        public async Task Read_non_existing_user_returns_null()
        {
            // Arrange
            var userId = 2;

            // Act
            var readUser = await _repository.Read(userId);

            // Assert
            Assert.Null(readUser);
        }

        [Fact]
        public async Task Read_existing_user_returns_correct_user_from_test_data()
        {
            // Arrange
            var userId = 1;

            // Act
            var readUser = await _repository.Read(userId);

            // Assert
            Assert.Equal("Dk", readUser.UserName);
            Assert.Equal(1, readUser.ContainerIds.Count);
        }

        [Fact]
        public async Task Update_non_existing_user_returns_notfound()
        {
            // Arrange
            var userUpdate = new UserUpdateDTO {
                Id = 2,
                UserName = "kaldmigbob"
            };

            // Act
            var result = await _repository.Update(userUpdate);

            // Assert
            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async Task Update_existing_user_returns_OK()
        {
            // Arrange
            var userUpdate = new UserUpdateDTO {
                Id = 1,
                UserName = "kaldmigbob"
            };

            // Act
            var result = await _repository.Update(userUpdate);

            // Assert
            Assert.Equal(OK, result);
        }

        [Fact]
        public async Task Delete_non_existing_user_returns_notfound()
        {
            // Arrange
            var userId = 2;

            // Act
            var result = await _repository.Delete(userId);

            // Assert
            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async Task Delete_existing_user_returns_OK()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = await _repository.Delete(userId);

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