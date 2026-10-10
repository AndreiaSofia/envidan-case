using System;
using System.IO;
using Xunit;

public class ShapeReportTests
{
    [Fact]
    public void Write_IncludesAreasAndComparison()
    {
        // Arrange
        var shapes = new IShape[] { new Square(2), new Circle(1) };
        var finder = new LargestShapeFinder();
        var largest = finder.FindLargest(shapes);
        var sw = new StringWriter();
        var report = new ShapeReport(sw);
        report.Write(shapes, largest);

        // Act
        var output = sw.ToString();

        // Assert
        Assert.Contains("The area of the square is", output);
        Assert.Contains("The area of the circle is", output);
        Assert.Contains("The square is the largest", output);
    }
}
