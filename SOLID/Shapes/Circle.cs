public record Circle(int Radius) : IShape
{
	public string Name => "circle";

	public double GetArea() => Math.PI * Math.Pow(Radius, 2);
}