using JuanJoseHernandez.Controllers.Api;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace EmployeeManagementSystem.Tests.UnitTests
{
    public class ApiEmployeesControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly ApiEmployeesController _controller;
        private readonly ClaimsPrincipal _userPrincipal;

        public ApiEmployeesControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new ApiEmployeesController(_mockEmployeeService.Object);

            // Setup ClaimsPrincipal for Controller Context
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            _userPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = _userPrincipal }
            };
        }

        [Fact]
        public async Task GetMe_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var userId = "test-user-id";
            var mockUser = new User
            {
                Id = userId,
                Names = "Test Name",
                Email = "test@example.com"
            };

            _mockEmployeeService.Setup(s => s.GetEmployeeByIdAsync(userId))
                .ReturnsAsync(mockUser);

            // Act
            var result = await _controller.GetMe();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetMe_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "test-user-id";
            _mockEmployeeService.Setup(s => s.GetEmployeeByIdAsync(userId))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.GetMe();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
