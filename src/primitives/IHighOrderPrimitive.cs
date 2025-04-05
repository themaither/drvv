namespace Drvv.Primitives;

interface IHighOrderPrimitive<T>
{
  IEnumerable<T> Lower();
}