using SOLID.Assignment;
using SOLID.Assignment.Shapes;
using Xunit;

namespace SOLID.Tests;

public class ShapeReportTests
{
    [Fact]
    public void Write_IncludesAreasAndComparison()
    {
        // Arrange
        IShape[] shapes = [new Square(2), new Circle(1)];
        var finder = new LargestShapeFinder();
        var largest = finder.FindLargest(shapes);
        var sw = new StringWriter();
        var report = new ShapeReport(sw);

        // Act
        report.Write(shapes, largest);
        var output = sw.ToString();

        // Assert
        Assert.Contains("The area of the square is", output);
        Assert.Contains("The area of the circle is", output);
        Assert.Contains("The square is the largest", output);
    }

    [Fact]
    public void Write_TwoEqualLargest_ReportsSameSize()
    {
        // Arrange
        IShape[] shapes = [new Square(2), new RightAngledTriangle(4, 2)];
        var sw = new StringWriter();
        var report = new ShapeReport(sw);

        // Act
        report.Write(shapes, new LargestShapeFinder().FindLargest(shapes));

        // Assert
        Assert.Contains("The square and triangle are the same size", sw.ToString());
    }

    [Fact]
    public void Write_NoShapes_ReportsNothingToCompare()
    {
        // Arrange
        var sw = new StringWriter();
        var report = new ShapeReport(sw);

        // Act
        report.Write([], []);

        // Assert
        Assert.Equal($"There are no shapes to compare{Environment.NewLine}", sw.ToString());
    }
}
