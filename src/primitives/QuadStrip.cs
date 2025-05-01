using Silk.NET.Maths;

namespace Drvv.Primitives;

record QuadStrip<T>(List<Quad<T>> Quads) : IHighOrderPrimitive<Quad<T>>, IOutlineable<T>
{
  public IEnumerable<Quad<T>> Lower() => Quads;

  public IEnumerable<Line<T>> Outline()
  {
    yield return Quads.First().Left;
    foreach (var quad in Quads)
    {
      yield return quad.Top;
      yield return quad.Bottom;
    }
    yield return Quads.Last().Right;
  }

  public QuadStrip<U> TransformType<U>(Func<T, U> transform)
  {
    return new QuadStrip<U>(
      Quads
        .Select(a => a.TransformType<U>(transform))
        .ToList()
    );
  }

  /// <summary> 
  /// Generates arc from quads
  /// </summary>
  public static QuadStrip<Vector2D<float>> GenerateArc(
    float start_angle, float end_angle,
    float inner_radius, float outer_radius,
    float resolution)
  {
    return new(
      new List<Quad<Vector2D<float>>>(GenerateArcQuads(inner_radius, outer_radius, start_angle, end_angle, resolution))
    );
  }

  private static IEnumerable<Quad<Vector2D<float>>> GenerateArcQuads(float inner_r, float outer_r, float angle1, float angle2, float res)
  {
    float k = 1.0f / res;
    while (angle1 < angle2)
    {
      yield return (new(
        new(
          MathF.Sin(angle1) * inner_r,
          MathF.Cos(angle1) * inner_r
        ),
        new(
          MathF.Sin(angle1) * outer_r,
          MathF.Cos(angle1) * outer_r
        ),
        new(
          MathF.Sin(MathF.Min(angle1 + k, angle2)) * outer_r,
          MathF.Cos(MathF.Min(angle1 + k, angle2)) * outer_r
        ),
        new(
          MathF.Sin(MathF.Min(angle1 + k, angle2)) * inner_r,
          MathF.Cos(MathF.Min(angle1 + k, angle2)) * inner_r
        )
      ));
      angle1 += k;
    }
  }
}

