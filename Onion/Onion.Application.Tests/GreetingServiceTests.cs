using Onion.Application.Interfaces;
using Onion.Application.Models;
using Onion.Application.Services;
using Onion.Domain.Entities;
using Onion.Domain.Exceptions;
using Xunit;

namespace Onion.Application.Tests;

public class GreetingServiceTests
{
    private sealed class FakeGreetingRepository : IGreetingRepository
    {
        public List<Greeting> Saved { get; } = [];

        public void Save(Greeting greeting) => Saved.Add(greeting);
    }

    [Fact]
    public void CreateGreeting_ValidName_ReturnsMessageAndSavesGreeting()
    {
        // Arrange
        var repository = new FakeGreetingRepository();
        var service = new GreetingService(repository);

        // Act
        var result = service.CreateGreeting(new CreateGreetingRequest { Name = "Ana" });

        // Assert
        Assert.Equal("Hello, Ana!", result.Message);
        var saved = Assert.Single(repository.Saved);
        Assert.Equal("Hello, Ana!", saved.Message);
    }

    [Fact]
    public void CreateGreeting_BlankName_ThrowsAndSavesNothing()
    {
        // Arrange
        var repository = new FakeGreetingRepository();
        var service = new GreetingService(repository);

        // Act / Assert
        Assert.Throws<DomainException>(() => service.CreateGreeting(new CreateGreetingRequest { Name = " " }));
        Assert.Empty(repository.Saved);
    }
}
