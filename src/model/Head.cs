using Silk.NET.Maths;

namespace Drvv.Model;

class Head
{
  public Disk Owner { get; }

  public Head(Disk owner)
  {
    Owner = owner;
  }

  public Vector2D<float> Target => new(MathF.Sin(Rotation) - 0.25f, MathF.Cos(Rotation) - 1f);

  public int PointingIndex { get; set; }

  public float Rotation { get; set; }
}