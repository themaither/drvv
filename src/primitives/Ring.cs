
namespace Drvv.Primitives;

record Ring<T>(List<QuadStrip<T>> Strips) : IHighOrderPrimitive<QuadStrip<T>>
{
  public IEnumerable<QuadStrip<T>> Lower() => Strips;
}

