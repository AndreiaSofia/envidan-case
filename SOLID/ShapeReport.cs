using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class ShapeReport
{
    private readonly TextWriter output;

    public ShapeReport(TextWriter output)
    {
        this.output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public void Write(IReadOnlyList<IShape> allShapes, IReadOnlyList<IShape> largestShapes)
    {
        if (allShapes == null) throw new ArgumentNullException(nameof(allShapes));
        if (largestShapes == null) throw new ArgumentNullException(nameof(largestShapes));

        foreach (var shape in allShapes)
        {
            output.WriteLine($"The area of the {shape.Name} is {shape.GetArea()}");
        }

        output.WriteLine(Comparison(largestShapes));
    }

    private string Comparison(IReadOnlyList<IShape> largestShapes) => largestShapes.Count switch
    {
        0 => "There are no shapes to compare",
        1 => $"The {largestShapes[0].Name} is the largest",
        _ => $"The {string.Join(" and ", largestShapes.Select(s => s.Name))} are the same size",
    };
}
