using SOLID.Assignment;
using SOLID.Assignment.Shapes;
using Xunit;

namespace SOLID.Tests;

public class LargestShapeFinderTests
{
    [Fact]
    public void FindLargest_Empty_ReturnsEmpty()
    {
        // Arrange
        var finder = new LargestShapeFinder();

        // Act
        var result = finder.FindLargest([]);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindLargest_SingleLargest_ReturnsThatShape()
    {
        // Arrange
        IShape[] shapes = [new Circle(2), new Square(3)];
        var finder = new LargestShapeFinder();

        // Act
        var result = finder.FindLargest(shapes);

        // Assert
        Assert.Single(result);
        Assert.Equal("circle", result[0].Name);
    }

    [Fact]
    public void FindLargest_MultipleEqualLargest_ReturnsBoth()
    {
        // Arrange
        IShape[] shapes = [new Square(2), new RightAngledTriangle(4, 2), new Circle(1)];
        var finder = new LargestShapeFinder();

        // Act
        var result = finder.FindLargest(shapes);

        // Assert
        Assert.Equal(2, result.Count);
        var names = result.Select(s => s.Name).OrderBy(n => n).ToArray();
        Assert.Equal(["square", "triangle"], names);
    }

    [Fact]
    public void FindLargest_Null_Throws()
    {
        // Arrange
        var finder = new LargestShapeFinder();

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => finder.FindLargest(null!));
    }
}
