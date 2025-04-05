namespace Drvv.Primitives;

interface IOutlineable<T>
{
  IEnumerable<Line<T>> Outline();
}