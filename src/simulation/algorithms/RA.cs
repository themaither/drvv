using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[AlgorithmInfo("RA", Description = 
"""
You've been blessed with this algorithm named by egyptian god of sun "Random Access".
""")]
class RA : Algorithm
{
  private Drive _drive;

  public RA(List<Model.Task> tasks, Drive drive)
    : base(tasks)
  {
    _drive = drive;
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