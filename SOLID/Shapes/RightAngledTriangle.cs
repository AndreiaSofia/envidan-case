public record RightAngledTriangle(int Width, int Height) : IShape
{
    public string Name => "triangle";

    public double GetArea() => Width * Height / 2.0;
}