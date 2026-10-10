using SOLID.Assignment;
using SOLID.Assignment.Shapes;

IShape[] shapes =
[
    new Circle(Random.Shared.Next(10) + 1),
    new RightAngledTriangle(Random.Shared.Next(10) + 1, Random.Shared.Next(10) + 1),
    new Square(Random.Shared.Next(10) + 1),
];

var finder = new LargestShapeFinder();
var report = new ShapeReport(Console.Out);

report.Write(shapes, finder.FindLargest(shapes));
