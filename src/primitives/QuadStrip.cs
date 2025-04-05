
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

  public static QuadStrip<T> Arc(float from, float to, float top, float bottom, float resolution)
  {
    throw new NotImplementedException();
  }
}

