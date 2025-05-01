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
}