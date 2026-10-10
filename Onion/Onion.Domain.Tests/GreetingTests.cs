using Onion.Domain.Entities;
using Xunit;

namespace Onion.Domain.Tests;

public class GreetingTests
{
    [Fact]
    public void For_Person_BuildsHelloMessage()
    {
        // Arrange
        var person = new Person("Ana");

        // Act
        var greeting = Greeting.For(person);

        // Assert
        Assert.Equal("Hello, Ana!", greeting.Message);
    }

    [Fact]
    public void For_Null_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => Greeting.For(null!));
    }
}
