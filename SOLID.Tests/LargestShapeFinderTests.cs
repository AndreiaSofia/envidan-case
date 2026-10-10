using System;
using System.Linq;
using Xunit;

public class LargestShapeFinderTests
{
    [Fact]
    public void FindLargest_Empty_ReturnsEmpty()
    {
        // Arrange
        var finder = new LargestShapeFinder();
        
        // Act
        var result = finder.FindLargest(Array.Empty<IShape>());
        
        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindLargest_SingleLargest_ReturnsThatShape()
    {
        // Arrange
        IShape[] shapes = new IShape[] { new Circle(2), new Square(3) };        
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
        IShape[] shapes = new IShape[] { new Square(2), new RightAngledTriangle(4, 2), new Circle(1) };
        var finder = new LargestShapeFinder();

        // Act
        var result = finder.FindLargest(shapes);

        // Assert
        Assert.Equal(2, result.Count);
        var names = result.Select(s => s.Name).OrderBy(n => n).ToArray();
        Assert.Equal(new[] { "square", "triangle" }, names);
    }
}
