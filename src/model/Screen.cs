using Silk.NET.Maths;

namespace Drvv.Model;

class Screen
{
  public float AspectRatio { get; set; }

  public Vector2D<float> ScreenToWorld(Vector2D<float> input)
  {
    return input with{X = input.X / AspectRatio };
  }

  public Vector2D<float> WorldToScreen(Vector2D<float> input)
  {
    return input with{X = input.X * AspectRatio };
  }

  public float[] ToMatrixArray()
  {
    return [
      1f / AspectRatio, 0f, 0f, 0f,
      0f,          1f, 0f, 0f,
      0f,          0f, 1f, 0f,
      0f,          0f, 0f, 1f
    ];
  }
}