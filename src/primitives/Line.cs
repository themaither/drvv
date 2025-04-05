
namespace Drvv.Primitives;

record Line<T>(T P1, T P2) : IDissolvable<T>
{
  public IEnumerable<T> Dissolve()
  {
    yield return P1;
    yield return P2;
  }
}
