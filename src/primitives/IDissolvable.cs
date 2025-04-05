namespace Drvv.Primitives;

interface IDissolvable<T>
{
  IEnumerable<T> Dissolve();
}