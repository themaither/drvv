
namespace Drvv.Primitives;

record Quad<T>(T P1, T P2, T P3, T P4) : IHighOrderPrimitive<Triangle<T>>
{
  public IEnumerable<Triangle<T>> Lower()
  {
    yield return new Triangle<T>(P1, P2, P3);
    yield return new Triangle<T>(P3, P4, P1);
  }

  public Line<T> Left => new(P1, P2);
  public Line<T> Right => new(P3, P4);
  public Line<T> Top => new(P2, P3);
  public Line<T> Bottom => new(P1, P4);

  public Quad<U> TransformType<U>(Func<T, U> transform)
  {
    return new Quad<U>(transform(P1), transform(P2), transform(P3), transform(P4));
  }
}

