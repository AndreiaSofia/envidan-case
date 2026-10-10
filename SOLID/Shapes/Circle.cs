public record Circle : IShape
{
    public int Radius { get; }

    public Circle(int radius)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radius); 
        Radius = radius; 
    }

    public string Name => "circle";

	public double GetArea() => Math.PI * Math.Pow(Radius, 2);
}