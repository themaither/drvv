using Silk.NET.Maths;

namespace Drvv.Primitives;

record Disk<T>(List<Ring<T>> Rings) : IHighOrderPrimitive<Ring<T>>
{
  public IEnumerable<Ring<T>> Lower() => Rings;

  public static Disk<Vector2D<float>> Generate(float inner_radius, float outer_radius, float rotation, int rows, int columns, float resolution)
  {
    return new(
      new List<Ring<Vector2D<float>>>(
        GenerateRings(inner_radius, outer_radius, rotation, rows, columns, resolution)
      )
    );
  }

  private static IEnumerable<Ring<Vector2D<float>>> GenerateRings(float inner_radius, float outer_radius, float rotation, int rows, int columns, float resolution)
  {
    float increment = (outer_radius - inner_radius) / rows; 

    for (int i = 0; i < rows; i++)
    {
      yield return Ring<Vector2D<float>>.Generate(inner_radius + increment * i, inner_radius + increment * (i+1), rotation, columns, resolution);
    }
  }
}
