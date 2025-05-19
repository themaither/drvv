using System.ComponentModel;
using System.Runtime.CompilerServices;
using Drvv.Model;
using Drvv.UI.For;

namespace Drvv.Simulation.Algorithms;

class SSTFOnDisk : Algorithm
{
  private List<Model.Task> _tasks;

  private Disk _disk;

  public float RowBias { get; set; } = 1;

  public float ColumnBias { get; set; } = 1;

  public SSTFOnDisk(List<Model.Task> tasks, Disk disk)
  {
    _tasks = tasks;
    _disk = disk;
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
    _disk.Running = true;
    
    
    if (_disk.Speed <= 0.9)
      return;

    if (_tasks.Count == 0)
    {
      Running = false;
      return;
    }

    var targetTask = PickClosestForDisk(_disk);

    _disk.Head.TargetRow = 
      ((targetTask.Sector) % (_disk.Rows * _disk.Columns)) / (int)_disk.Columns;

    if (_disk.Head.TargetSector == targetTask.Sector)
    {
      if (targetTask is ReadTask read)
      {
        Console.WriteLine($"Read {_disk.Data[_disk.Head.TargetSector].Value}");
      } 
      else if (targetTask is WriteTask write)
      {
        _disk.Data[_disk.Head.TargetSector] = write.Value;
      }
      _tasks.Remove(targetTask);
    }
  }
}