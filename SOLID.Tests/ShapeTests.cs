using SOLID.Assignment.Shapes;
using Xunit;

namespace SOLID.Tests;

public class ShapeTests
{
    [Fact]
    public void Circle_GetArea_ReturnsPiRSquared()
    {
        // Arrange
        var circle = new Circle(2);

        // Act
        var area = circle.GetArea();

        // Assert
        Assert.Equal(Math.PI * 4, area, precision: 10);
    }

    [Fact]
    public void RightAngledTriangle_GetArea_ReturnsHalfBaseTimesHeight()
    {
        // Arrange
        var triangle = new RightAngledTriangle(3, 4);

        // Act
        var area = triangle.GetArea();

        // Assert
        Assert.Equal(6.0, area);
    }

    [Fact]
    public void Square_GetArea_ReturnsSideSquared()
    {
        // Arrange
        var square = new Square(5);

        // Act
        var area = square.GetArea();

        // Assert
        Assert.Equal(25.0, area);
    }

    [Fact]
    public void Square_GetArea_LargeSide_DoesNotOverflow()
    {
        // Arrange: 100000 * 100000 does not fit in an int
        var square = new Square(100_000);

        // Act
        var area = square.GetArea();

        // Assert
        Assert.Equal(1e10, area);
    }

    [Fact]
    public void RightAngledTriangle_GetArea_LargeSides_DoesNotOverflow()
    {
        // Arrange
        var triangle = new RightAngledTriangle(100_000, 100_000);

        // Act
        var area = triangle.GetArea();

        // Assert
        Assert.Equal(5e9, area);
    }

    [Fact]
    public void Circle_GetArea_LargeRadius_DoesNotOverflow()
    {
        // Arrange
        var circle = new Circle(int.MaxValue);

        // Act
        var area = circle.GetArea();

        // Assert
        Assert.Equal(Math.PI * Math.Pow(int.MaxValue, 2), area);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Circle_NonPositiveRadius_Throws(int radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(radius));
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(-2, 3)]
    public void RightAngledTriangle_NonPositiveSides_Throws(int width, int height)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new RightAngledTriangle(width, height));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Square_NonPositiveSide_Throws(int side)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Square(side));
    }

    [Fact]
    public void Shapes_ExposeLowercaseNames()
    {
        Assert.Equal("circle", new Circle(1).Name);
        Assert.Equal("triangle", new RightAngledTriangle(1, 1).Name);
        Assert.Equal("square", new Square(1).Name);
    }
}
