
namespace Drvv.Primitives;

record Disk<T>(List<Ring<T>> Rings) : IHighOrderPrimitive<Ring<T>>
{
  public IEnumerable<Ring<T>> Lower() => Rings;
}
