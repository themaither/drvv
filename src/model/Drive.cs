namespace Drvv.Model;

class Drive
{
  public Drive(int disks, int rows, int columns)
  {
    Disks = new Disk[disks];
    for (int i = 0; i < Disks.Length; i++)
    {
      Disks[i] = new Disk(rows, columns);
    }
    Cylinders = rows * columns;
  }

  public Disk[] Disks { get; }

  public int Cylinders { get; }

  public int Rows => Disks.First().Rows;

  public int Columns => Disks.First().Columns;

  public int Sectors => Disks.First().Sectors;

  public bool Running => Disks.First().Running;

  public float Speed => Disks.First().Speed;

  public void Start() 
  {
    foreach (var disk in Disks)
    {
      disk.Start(); 
    }
  }

  public void Stop() {
    foreach (var disk in Disks)
    {
      disk.Stop(); 
    }
  }
}