using Silk.NET.Maths;

namespace Drvv.Model;

class Screen
{
  public float AspectRatio { get; set; }

  public Vector2D<int> Resolution { get; set; }

  public Vector2D<float> CameraPosition { get; set; } = new(0, 0);

  public Vector2D<float> ScreenToWorld(Vector2D<float> input)
  {
    var distorted = input; //with{X = input.X / AspectRatio };
    distorted.Y = Resolution.Y - distorted.Y;
    distorted.X /= Resolution.X * .5f - .5f;
    distorted.Y /= Resolution.Y * .5f;
    distorted.Y -= 1f;
    distorted.X -= 1f;
    distorted.X *= AspectRatio;
    return distorted;
  }

  public Vector2D<float> WorldToScreen(Vector2D<float> input)
  {
    return input with{X = input.X * AspectRatio };
    //TODO: apply resolution
  }

  public float[] ToMatrixArray()
  {
    return [
      0.5f / AspectRatio, 0f, 0f, 0f,
      0f,               0.5f, 0f, 0f,
      0f,               0f, 1f, 0f,
      CameraPosition.X, CameraPosition.Y, 0f, 1f
    ];
  }
}