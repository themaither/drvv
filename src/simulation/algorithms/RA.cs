using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[AlgorithmInfo("RA", Description = 
"""
You've been blessed with this algorithm named by egyptian god of sun "Random Access".
""")]
class RA : Algorithm
{
  private Drive _drive;

  private List<FCFSOnDisk> _diskAlgorithms;

  public RA(List<Model.Task> tasks, Drive drive)
    : base(tasks)
  {
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
    foreach (var task in _tasks)
    {
      if (task is ReadTask read)
      {
        Read(_drive, task.Sector);
      } 
      else if (task is WriteTask write)
      {
        DirectWrite(_drive, task.Sector, write.Value);
      }
    }
    _tasks.Clear();
    Running = false;
  }
}