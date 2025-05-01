using Silk.NET.Maths;

namespace Drvv.Primitives;

record Ring<T>(List<QuadStrip<T>> Strips) : IHighOrderPrimitive<QuadStrip<T>>
{
  public IEnumerable<QuadStrip<T>> Lower() => Strips;

  public static Ring<Vector2D<float>> Generate(float inner_radius, float outer_radius, float rotation, int count, float resolution)
  {
    return new Ring<Vector2D<float>>(
      new List<QuadStrip<Vector2D<float>>>(
        GenerateQuadStrips(inner_radius, outer_radius, rotation, count, resolution)
      )
    );
  }

  private static IEnumerable<QuadStrip<Vector2D<float>>> GenerateQuadStrips(float inner_radius, float outer_radius, float rotation, int count, float resolution)
  {
    float increment = MathF.PI * 2 / count; 

    for (int i = 0; i < count; i++)
    {
      yield return QuadStrip<Vector2D<float>>.GenerateArc(increment * i + rotation, increment * (i + 1) + rotation, inner_radius, outer_radius, resolution);
    }
  }
}

