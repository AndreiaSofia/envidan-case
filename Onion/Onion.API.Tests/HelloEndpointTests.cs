using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Onion.API.Tests;

public sealed class HelloEndpointTests : IDisposable
{
    // Program is internal (top-level statements); the API project exposes it
    // to this assembly through InternalsVisibleTo.
    private readonly WebApplicationFactory<Program> _factory = new();
    private readonly HttpClient _client;

    public HelloEndpointTests()
    {
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_WithName_ReturnsGreetingAsPlainText()
    {
        // Act
        var response = await _client.GetAsync("/hello?name=Ana");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("text/plain", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal("Hello, Ana!", await response.Content.ReadAsStringAsync());
    }

    [Theory]
    [InlineData("/hello")]
    [InlineData("/hello?name=")]
    [InlineData("/hello?name=%20%20")]
    public async Task Get_WithoutUsableName_ReturnsBadRequestFromDomainRule(string url)
    {
        // Act
        var response = await _client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("name is required", await response.Content.ReadAsStringAsync());
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}
