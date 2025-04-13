namespace Drvv.Model;

class Disk
{
  public Disk()
  {
    Head = new(this);
    InnerRadius = 0.1f;
    OuterRadius = 0.9f;
    Rows = 8;
    Columns = 48;
  }

  public Head Head { get; }

  public float InnerRadius { get; set; }

  public float OuterRadius { get; set; }

  public uint Rows { get; set; }

  public uint Columns { get; set; }

  public float Rotation { get; set; }
}