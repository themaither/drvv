using Silk.NET.Maths;

namespace Drvv.Model;

class Disk
{
  public Disk()
  {
    Head = new(this);
    Rows = 16;
    Columns = 48;
    Scale = 0.75f;
  }

  public Head Head { get; }

  public float InnerRadius => 0.3f * Scale;

  public float OuterRadius => 0.9f * Scale;

  public uint Rows { get; set; }

  public uint Columns { get; set; }

  public float Rotation { get; set; }

  public float Scale { get; set; }

  public Vector2D<float> Position { get; set; }
  
  public int TargetRow { get; set; }
}
