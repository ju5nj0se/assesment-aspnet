using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EmployeeManagementSystem.Tests.IntegrationTests
{
    public class BasicIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_LoginPage_ReturnsSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Auth/Login");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", 
                response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task Get_ApiMe_WithoutToken_ReturnsUnauthorized()
        {
             // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/me");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
