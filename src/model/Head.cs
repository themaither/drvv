using Silk.NET.Maths;

namespace Drvv.Model;

class Head
{
  public Disk Owner { get; }

  public Head(Disk owner)
  {
    Owner = owner;
  }

  public Vector2D<float> Target { get; set; }

  public int PointingIndex { get; set; }
}