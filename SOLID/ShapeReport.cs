using SOLID.Assignment.Shapes;

namespace SOLID.Assignment;

public class ShapeReport
{
    private readonly TextWriter output;

    public ShapeReport(TextWriter output)
    {
        ArgumentNullException.ThrowIfNull(output);
        this.output = output;
    }

    public void Write(IReadOnlyList<IShape> allShapes, IReadOnlyList<IShape> largestShapes)
    {
        ArgumentNullException.ThrowIfNull(allShapes);
        ArgumentNullException.ThrowIfNull(largestShapes);

        foreach (var shape in allShapes)
        {
            output.WriteLine($"The area of the {shape.Name} is {shape.GetArea()}");
        }

        output.WriteLine(Comparison(largestShapes));
    }

    private static string Comparison(IReadOnlyList<IShape> largestShapes) => largestShapes.Count switch
    {
        0 => "There are no shapes to compare",
        1 => $"The {largestShapes[0].Name} is the largest",
        _ => $"The {string.Join(" and ", largestShapes.Select(s => s.Name))} are the same size",
    };
}
