public record Square(int Side) : IShape
{
    public string Name => "square";

    public double GetArea() => Side * Side;
}
