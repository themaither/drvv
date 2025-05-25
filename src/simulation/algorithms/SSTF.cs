using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[AlgorithmInfo("SSTF", Description = 
"""
Shortest Seek Time First, or Shortest Seek First (SSF), a more advanced algorithm than previously appeared FCFS. It uses advanced machine learning and statistical analysis to perform evaluation of one "shortest to seek" task, which is usually one that is shortest to seek, and executes it. AI have tendency to forget "Long seek last" tasks which may harm data integrity. Don't forget to remind algoritm about them!
""")]
class SSTF : Algorithm
{
  private Drive _drive;

  public float RowBias { get; set; } = 1;

  public float ColumnBias { get; set; } = 80;

  public SSTF(List<Model.Task> tasks, Drive drive)
    : base(tasks)
  {
    _drive = drive;
  }

  private static int Wrap(int x, int max)
  {
    return x >= 0 ? x : x + max;
  }

  private Model.Task PickClosestForDrive(Model.Drive drive) {
    var task = _tasks
      .Select(a =>
        (
          bias: Bias(a.Sector % drive.Cylinders, drive.Disks.First().Head.TargetSector, drive),
          task: a
        )
      )
      .OrderBy(a => a.bias)
      .First();

    return task.task;
  }

  private float Bias(int sector, int headSector, Model.Drive drive) {
    return ColumnBias * Wrap((sector % drive.Columns) - (headSector % drive.Columns), drive.Columns) + RowBias * Math.Abs(sector / drive.Columns - headSector / drive.Columns);
  }

  protected override void OnUpdate(float deltaTime)
  {
    _drive.Start();
    
    if (_drive.Speed <= 0.9)
      return;

    if (_tasks.Count == 0)
    {
      Running = false;
      return;
    }

    var targetTask = PickClosestForDrive(_drive);

    for (int i = 0; i < _drive.Disks.Length; i++)
    {
      Disk? disk = _drive.Disks[i];
      
      disk.Head.TargetRow = 
       ((targetTask.Sector) % (disk.Rows * disk.Columns)) / (int)disk.Columns;
      Execute(_drive, i, targetTask);
    }
  }
}