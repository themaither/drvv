using Silk.NET.Maths;

namespace Drvv.Model;

class Head
{
  public Disk Owner { get; }

  public Head(Disk owner)
  {
    Owner = owner;
    Speed = 10;
  }

  public Vector2D<float> Target => new((MathF.Sin(Rotation) - 0.25f) * Owner.Scale, (MathF.Cos(Rotation) - 1f) * Owner.Scale);

  public int PointingIndex { get; set; }

  public float Rotation { get; set; }

  public float Speed { get; set; }

  public int TargetRow { get; set; }
}
