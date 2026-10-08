// See https://aka.ms/new-console-template for more information

var circle = new Circle(Random.Shared.Next(10) + 1);
var triangle = new RightAngledTriangle(Random.Shared.Next(10) + 1, Random.Shared.Next(10) + 1);

var circleArea = Math.PI * Math.Pow(circle.Radius, 2);
var triangleArea = triangle.Width * triangle.Height / 2.0;

Console.WriteLine($"The area of the circle is {circleArea}");
Console.WriteLine($"The area of the triangle is {triangleArea}");

if (circleArea > triangleArea)
{
    Console.WriteLine("The circle is the largest");
}
else if (circleArea < triangleArea)
{
    Console.WriteLine("The triangle is the largest");
}
else
{
    Console.WriteLine("The circle and triangle are the same size");
}

public interface IShape
{
    double GetArea();
}

public record Circle(int Radius) : IShape { }

public record RightAngledTriangle(int Width, int Height) : IShape { }
