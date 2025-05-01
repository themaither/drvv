using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

class FCFS : Algorithm
{
  private List<Model.Task> _tasks;

  private Disk _disk;

  public FCFS(List<Model.Task> tasks, Disk disk)
  {
    _tasks = tasks;
    _disk = disk;
  }

  protected override void OnUpdate(float deltaTime)
  {
    _disk.Running = true;
    if (_tasks.Count == 0) 
    {
      _disk.Head.TargetRow = -1;
      _disk.Running = false;
      Running = false;
      return;
    }

    if (_disk.Speed <= 0.9f)
      return;

    _disk.Head.TargetRow = _tasks.First().Sector / (int)_disk.Columns;
    if (_disk.Head.TargetSector == _tasks.First().Sector)
    {
      if (_tasks.First() is ReadTask read)
      {
        Console.WriteLine($"Read {_disk.Data[_disk.Head.TargetSector].Value}");
      } 
      else if (_tasks.First() is WriteTask write)
      {
        _disk.Data[_disk.Head.TargetSector] = write.Value;
      }
      _tasks.Remove(_tasks.First());
    }
  }
}