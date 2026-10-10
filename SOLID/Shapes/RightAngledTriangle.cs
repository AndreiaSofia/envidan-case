namespace SOLID.Assignment.Shapes;

public record RightAngledTriangle : IShape
{
    public int Width { get; }

    public int Height { get; }

    public RightAngledTriangle(int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        Width = width;
        Height = height;
    }

    public string Name => "triangle";

    // Multiply as double so large sides do not overflow int before the division.
    public double GetArea() => (double)Width * Height / 2;
}
