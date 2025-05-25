using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[AlgorithmInfo("LOOK", Description = 
"""
LOOK algortihm LOOKs like a SCAN algorithm, but is considered more optimized, because now riders know what elevator buttons used for.
""")]
class LOOK : Algorithm
{
  private Drive _drive;

  public float RowBias => _drive.Columns * _drive.Disks.Count();

  public float ColumnBias => 1;

  private int _direction = 1;

  public LOOK(List<Model.Task> tasks, Drive drive)
    : base(tasks)
  {
    _drive = drive;
  }

  private static int Wrap(int x, int max)
  {
    return x >= 0 ? x : x + max;
  }

  private Model.Task? PickClosestForDrive(Model.Drive drive) {
    var tasks = _tasks
      .Select(a =>
        (
          bias: Bias(a.Sector % drive.Cylinders, drive.Disks.First().Head.TargetSector, drive),
          task: a
        )
      )
      .Where(a => a.bias > 0)
      .OrderBy(a => a.bias);

    if (!tasks.Any()) {
      return null;
    }

    var task = tasks.First();

    return task.task;
  }

  private float Bias(int sector, int headSector, Model.Drive drive) {
    return 1 + ColumnBias * Wrap((sector % drive.Columns) - (headSector % drive.Columns), drive.Columns) + RowBias * _direction * (sector / drive.Columns - headSector / drive.Columns);
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

    if (targetTask == null)
    {
      _direction = -_direction;
      return;
    }

    for (int i = 0; i < _drive.Disks.Length; i++)
    {
      Disk? disk = _drive.Disks[i];
      disk.Head.TargetRow = 
       ((targetTask.Sector) % (disk.Rows * disk.Columns)) / (int)disk.Columns;

      Execute(_drive, i, targetTask);
    }
  }
}