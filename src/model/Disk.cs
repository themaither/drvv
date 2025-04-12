namespace Drvv.Model;

class Disk
{
  public Disk()
  {
    Pointer = new() {Target = new(0f, 0.5f)};
    InnerRadius = 0.1f;
    OuterRadius = 0.9f;
    Rows = 8;
    Columns = 48;
  }

  public Pointer Pointer { get; }

  public float InnerRadius { get; set; }

  public float OuterRadius { get; set; }

  public uint Rows { get; set; }

  public uint Columns { get; set; }
}