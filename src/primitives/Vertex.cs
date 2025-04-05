namespace Drvv.Primitives;
using Silk.NET.Maths;

struct Vertex(Vector2D<float> position, Color3<float> color)
{
  public Vector2D<float> Position { get; set; } = position;
  public Color3<float> Color { get; set; } = color;
}
