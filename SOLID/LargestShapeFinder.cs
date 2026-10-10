using System;
using System.Collections.Generic;
using System.Linq;

public class LargestShapeFinder
{
    private const double Tolerance = 0.0001;

    public IReadOnlyList<IShape> FindLargest(IEnumerable<IShape> shapes)
    {
        if (shapes == null) return Array.Empty<IShape>();

        var list = shapes.ToList();

        if (list.Count == 0)
        {
            return Array.Empty<IShape>();
        }

        // Compute areas once to avoid repeated GetArea() calls
        var areas = list.Select(s => (Shape: s, Area: s.GetArea())).ToList();
        var max = areas.Max(x => x.Area);

        return areas.Where(x => Math.Abs(x.Area - max) < Tolerance).Select(x => x.Shape).ToList();
    }
}
