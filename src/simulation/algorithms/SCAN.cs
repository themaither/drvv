using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[AlgorithmInfo("SCAN", Description = 
"""

""")]
class SCAN : Algorithm
{
  private Drive _drive;

  private int _direction = 1;

  private int _deltaSector = 0;

  private int _lookingRow = 0;

  private bool _initiated = false;

  public SCAN(List<Model.Task> tasks, Drive drive)
    : base(tasks)
  {
    _drive = drive;
  }

  private static int Wrap(int x, int max)
  {
    return x >= 0 ? x : x + max;
  }

  protected override void OnUpdate(float deltaTime)
  {
    _drive.Start();
    
    if (_drive.Speed <= 0.9)
      return;

    if (_tasks.Count == 0)
    {
      Running = false;
      _initiated = false;
      return;
    }

    if (!_initiated) {
      foreach (var disk in _drive.Disks)
      {
        disk.Head.TargetRow = 0;
        if (_drive.Disks.First().Head.TargetSector == 0) {
          _initiated = true;
          _deltaSector = 0;
          break;
        }
      }
      return;
    }

    foreach (var disk in _drive.Disks)
    {
      disk.Head.TargetRow = _lookingRow; 
    }

    if (_deltaSector - _drive.Disks.First().Head.TargetSector == _drive.Columns - 1)
    {
      _lookingRow += _direction;
      if (_direction > 0 && _lookingRow >= _drive.Rows - 1)
      {
        _direction = -1;
      }
      if (_direction < 0 && _lookingRow == 0) 
      {
        _direction = 1;
      }
    }

    _deltaSector = _drive.Disks.First().Head.TargetSector;

    foreach (var task in _tasks)
    {
      if (task.Sector % _drive.Cylinders == _drive.Disks.First().Head.TargetSector)
      {
        Execute(_drive, task.Sector / _drive.Cylinders, task);
        break;
      } 
    }

  }
}