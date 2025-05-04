using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

class FCFSOnDisk : Algorithm
{
  private List<Model.Task> _tasks;

  private Disk _disk;

  private int _offset;

  public FCFSOnDisk(List<Model.Task> tasks, Disk disk, int offset)
  {
    _tasks = tasks;
    _disk = disk;
    _offset = offset;
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

  }
}