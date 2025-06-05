using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

[AlgorithmInfo("FCFS", Description = 
"""
FCFS (First Come First Serve, don't mistake for FIFO, which is the same thing) --- an algorithm that serves request in order of their arrival time. Great at dealing with pending time of request, but lacks speed if last request was close to R/W head.

""")]
class FCFS : Algorithm
{
  private Drive _drive;

  private List<FCFSOnDisk> _diskAlgorithms;

  public FCFS(List<Model.Task> tasks, Drive drive)
    : base(tasks)
  {
    _drive = drive;
    _diskAlgorithms = new(drive.Disks.Length);
    for (int i = 0; i < _drive.Disks.Length; i++)
    {
      _diskAlgorithms.Add(new(_tasks, i, _drive, this, i * _drive.Cylinders));
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