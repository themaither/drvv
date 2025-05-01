namespace Drvv;

static class EnumerableExtensions
{
  /// <summary>
  /// Adds a index specifier for every member
  /// </summary>
  public static IEnumerable<(T Value, int Index)> Index<T>(this IEnumerable<T> input)
  {
    int index = 0;
    foreach (var item in input)
    {
      yield return (item, index++);
    }
  }
}
