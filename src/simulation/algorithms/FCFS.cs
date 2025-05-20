using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[Name("FCFS")]
class FCFS : Algorithm
{
  private List<Model.Task> _tasks;

  private Drive _drive;

  private List<FCFSOnDisk> _diskAlgorithms;

  public FCFS(List<Model.Task> tasks, Drive drive)
  {
    _tasks = tasks;
    _drive = drive;
    _diskAlgorithms = new(drive.Disks.Length);
    for (int i = 0; i < _drive.Disks.Length; i++)
    {
      Disk disk = _drive.Disks[i];
      _diskAlgorithms.Add(new(_tasks, disk, i * _drive.Cylinders));
    }
  }

  protected override void OnUpdate(float deltaTime)
  {
    foreach (var diskAlgorithm in _diskAlgorithms)
    {
      diskAlgorithm.Running = true;
      diskAlgorithm.Update(deltaTime);
      if (_tasks.Count == 0)
      {
        Running = false; 
      }
    }
  }
}