using Onion.Domain.Entities;
using Onion.Domain.Exceptions;
using Xunit;

namespace Onion.Domain.Tests;

public class PersonTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_BlankName_ThrowsDomainException(string? name)
    {
        Assert.Throws<DomainException>(() => new Person(name));
    }

    [Fact]
    public void Constructor_ValidName_TrimsWhitespace()
    {
        // Arrange / Act
        var person = new Person("  Ana  ");

        // Assert
        Assert.Equal("Ana", person.Name);
    }
}
