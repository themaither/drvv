
namespace Drvv.Primitives;

record Triangle<T>(T P1, T P2, T P3) : IDissolvable<T>
{
  public IEnumerable<T> Dissolve()
  {
    yield return P1;
    yield return P2;
    yield return P3;
  }
}
