
namespace Drvv.Primitives;

record Quad<T>(T P1, T P2, T P3, T P4) : IHighOrderPrimitive<Triangle<T>>
{
  public IEnumerable<Triangle<T>> Lower()
  {
    yield return new Triangle<T>(P1, P2, P3);
    yield return new Triangle<T>(P3, P4, P1);
  }

  public Line<T> Left => throw new NotImplementedException();
  public Line<T> Right => throw new NotImplementedException();
  public Line<T> Top => throw new NotImplementedException();
  public Line<T> Bottom => throw new NotImplementedException();
}

