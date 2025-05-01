using Silk.NET.Maths;

namespace Drvv.Primitives;

record Triangle<T>(T P1, T P2, T P3) : IDissolvable<T>
{
  public IEnumerable<T> Dissolve()
  {
    yield return P1;
    yield return P2;
    yield return P3;
  }

  public static Triangle<Vector2D<float>> GenerateDirectional(Vector2D<float> start, Vector2D<float> end, float width)
  {
    Vector2D<float> between = end - start;
    Vector2D<float> normalized = between / between.Length;
    Vector2D<float> perpendicular = new(normalized.Y, -normalized.X);
    perpendicular *= width;

    return new(
      start, end, start + perpendicular
    );
  }
}
