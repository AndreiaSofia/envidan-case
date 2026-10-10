public record Square : IShape
{
    public int Side { get; }

    public Square(int side)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(side);
        Side = side;
    }

    public string Name => "square";

    public double GetArea() => Side * Side;
}
