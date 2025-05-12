using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;
using Drvv.Model;
using Drvv.UI.For;

namespace Drvv.Simulation.Algorithms;

class SSTF : Algorithm
{
  private List<Model.Task> _tasks;

  private Drive _drive;

  public float RowBias { get; set; } = 1;

  public float ColumnBias { get; set; } = 1;

  public SSTF(List<Model.Task> tasks, Drive drive)
  {
    _tasks = tasks;
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
      _drive.Stop();
      Running = false;
      return;
    }

    var targetTask = PickClosestForDrive(_drive);

    for (int i = 0; i < _drive.Disks.Length; i++)
    {
      Disk? disk = _drive.Disks[i];
      disk.Head.TargetRow = 
       ((targetTask.Sector) % (disk.Rows * disk.Columns)) / (int)disk.Columns;

      if (disk.Head.TargetSector == targetTask.Sector % _drive.Cylinders && targetTask.Sector / _drive.Cylinders == i)
      {
        if (targetTask is ReadTask read)
        {
          Console.WriteLine($"Read {disk.Data[disk.Head.TargetSector % _drive.Cylinders].Value}");
        } 
        else if (targetTask is WriteTask write)
        {
          disk.Data[disk.Head.TargetSector % _drive.Cylinders] = write.Value;
        }
        _tasks.Remove(targetTask);
      }
    }
  }
}