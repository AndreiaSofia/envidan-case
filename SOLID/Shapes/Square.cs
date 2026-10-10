namespace SOLID.Assignment.Shapes;

public record Square : IShape
{
    public int Side { get; }

    public Square(int side)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(side);
        Side = side;
    }

    public string Name => "square";

    // Multiply as double so large sides do not overflow int before the conversion.
    public double GetArea() => (double)Side * Side;
}
