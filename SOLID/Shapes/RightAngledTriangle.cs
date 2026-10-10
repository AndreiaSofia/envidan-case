public record RightAngledTriangle : IShape
{
    public int Width { get; }
    public int Height { get; }

    public RightAngledTriangle(int with, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(with); 
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height); 
        Width = with; 
        Height = height; 
    }

    public string Name => "triangle";

    public double GetArea() => Width * Height / 2.0;
}