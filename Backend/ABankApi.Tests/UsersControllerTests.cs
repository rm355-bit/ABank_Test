using Xunit;
using Moq;
using ABankApi.Controllers;
using ABankApi.Data;
using ABankApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABankApi.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _controller = new UsersController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<UserDto> { new UserDto { Id = 1, Nombres = "Test", Apellidos = "User" } };
            _mockRepo.Setup(r => r.GetAllUsers()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetUserById_UserExists_ReturnsOkResult()
        {
            // Arrange
            var user = new UserDto { Id = 1, Nombres = "Test", Apellidos = "User" };
            _mockRepo.Setup(r => r.GetUserById(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUserById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task GetUserById_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetUserById(2)).ReturnsAsync((UserDto)null);

            // Act
            var result = await _controller.GetUserById(2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { Id = 1, Nombres = "Test", Apellidos = "User" };
            var userDto = new UserDto { Id = 1, Nombres = "Test", Apellidos = "User" };
            _mockRepo.Setup(r => r.CreateUser(user)).ReturnsAsync(userDto);

            // Act
            var result = await _controller.CreateUser(user);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(userDto, createdResult.Value);
        }

        [Fact]
        public async Task UpdateUser_UserExists_ReturnsOkResult()
        {
            // Arrange
            var user = new User { Id = 1, Nombres = "Test", Apellidos = "User" };
            var userDto = new UserDto { Id = 1, Nombres = "Test", Apellidos = "User" };
            _mockRepo.Setup(r => r.updateUser(1, user)).ReturnsAsync(true);
            _mockRepo.Setup(r => r.GetUserById(1)).ReturnsAsync(userDto);

            // Act
            var result = await _controller.UpdateUser(1, user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userDto, okResult.Value);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var user = new User { Id = 2, Nombres = "Test", Apellidos = "User" };
            _mockRepo.Setup(r => r.updateUser(2, user)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateUser(2, user);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteUser_UserExists_ReturnsOkResult()
        {
            // Arrange
            var userDto = new UserDto { Id = 1, Nombres = "Test", Apellidos = "User" };
            _mockRepo.Setup(r => r.GetUserById(1)).ReturnsAsync(userDto);
            _mockRepo.Setup(r => r.DeleteUser(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userDto, okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetUserById(2)).ReturnsAsync((UserDto)null);

            // Act
            var result = await _controller.DeleteUser(2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
