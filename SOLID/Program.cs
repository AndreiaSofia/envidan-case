// See https://aka.ms/new-console-template for more information

var circle = new Circle(Random.Shared.Next(10) + 1);
var triangle = new RightAngledTriangle(Random.Shared.Next(10) + 1, Random.Shared.Next(10) + 1);
var square = new Square(Random.Shared.Next(10) + 1);

List<IShape> shapes = [circle, triangle, square];

foreach (var shape in shapes)
{
    Console.WriteLine($"The area of the {shape.Name} is {shape.GetArea()}");
}

Console.WriteLine(ComparisonLargestShape(shapes));
return;

string ComparisonLargestShape(List<IShape> allShapes)
{
    var largestArea = allShapes.Max(shape => shape.GetArea());
    var largestShapes = allShapes.Where(x => x.GetArea() == largestArea).ToList();

    return largestShapes.Count == 1
        ? $"The {largestShapes[0].Name} is the largest"
        : $"The {string.Join(" and ", largestShapes.Select(x => x.Name))} are the same area";
}

// Interface
public interface IShape
{
    string Name { get; }
    double GetArea();
}

// Implementations
public record Circle(int Radius) : IShape
{
    public string Name => "circle";

    public double GetArea() => Math.PI * Math.Pow(Radius, 2);
}

public record RightAngledTriangle(int Width, int Height) : IShape
{
    public string Name => "triangle";

    public double GetArea() => Width * Height / 2.0;
}

public record Square(int Side) : IShape
{
    public string Name => "square";

    public double GetArea() => Side * Side;
}
