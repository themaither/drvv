using System.ComponentModel;
using System.Runtime.CompilerServices;
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

  private Model.Task PickClosestForDisk(Model.Disk disk) {
    var task = _tasks
      .Select(a =>
        (
          bias: Bias(a.Sector, disk.Head.TargetSector, disk),
          task: a
        )
      )
      .OrderBy(a => a.bias)
      .First();

    return task.task;
  }

  private float Bias(int sector, int headSector, Model.Disk disk) {
    return ColumnBias * Wrap((sector % disk.Columns) - (headSector % disk.Columns), disk.Columns) + RowBias * Math.Abs(sector / disk.Columns - headSector / disk.Columns);
  }

  protected override void OnUpdate(float deltaTime)
  {
    foreach (var disk in _drive.Disks)
    {
      disk.Running = true;
    }
    
    if (_drive.Disks.Any(p => p.Speed <= 0.9))
      return;

    if (_tasks.Count == 0)
    {
      foreach (var disk in _drive.Disks)
      {
        disk.Running = false;
      }
      Running = false;
      return;
    }

    var targetTask = PickClosestForDisk(_drive.Disks[0]);

    _drive.Disks[0].Head.TargetRow = 
      ((targetTask.Sector) % (_drive.Disks[0].Rows * _drive.Disks[0].Columns)) / (int)_drive.Disks[0].Columns;

    if (_drive.Disks[0].Head.TargetSector == targetTask.Sector)
    {
      if (targetTask is ReadTask read)
      {
        Console.WriteLine($"Read {_drive.Disks[0].Data[_drive.Disks[0].Head.TargetSector].Value}");
      } 
      else if (targetTask is WriteTask write)
      {
        _drive.Disks[0].Data[_drive.Disks[0].Head.TargetSector] = write.Value;
      }
      _tasks.Remove(targetTask);
    }
  }
}