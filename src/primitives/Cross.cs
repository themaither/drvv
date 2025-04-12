using Silk.NET.Maths;

namespace Drvv.Primitives;

record Cross(Vector2D<float> Position) : IHighOrderPrimitive<Line<Vector2D<float>>>
{
  public IEnumerable<Line<Vector2D<float>>> Lower()
  {
    yield return new(
      Position + new Vector2D<float>(0.1f, 0.1f),
      Position + new Vector2D<float>(-0.1f, -0.1f)
    );
    yield return new(
      Position + new Vector2D<float>(-0.1f, 0.1f),
      Position + new Vector2D<float>(0.1f, -0.1f)
    );
  }
}
