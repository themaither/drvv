using System.Reflection.PortableExecutable;
using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

abstract class Algorithm
{
  public bool Running { get; set; }

  protected List<Model.Task> _tasks;

  protected Algorithm(List<Model.Task> tasks)
  {
    _tasks = tasks;
  }

  public void Update(float deltaTime)
  {
    if (!Running) return;
    OnUpdate(deltaTime);
  }
  protected abstract void OnUpdate(float deltaTime);

  public void Write(Drive drive, int sector, Data value) {
    drive.Disks[sector / drive.Cylinders].Data[drive.Disks[sector / drive.Cylinders].Head.TargetSector % drive.Cylinders] = value;
  }

  public void DirectWrite(Drive drive, int sector, Data value) {
    drive.Disks[sector / drive.Cylinders].Data[sector % drive.Cylinders] = value;
  }

  public void Read(Drive drive, int sector) {
    Console.WriteLine($"Read {sector}");
  }

  public void Execute(Drive drive, int diskIndex, Model.Task task) {
    if (drive.Disks[diskIndex].Head.TargetSector == task.Sector % drive.Cylinders && task.Sector / drive.Cylinders == diskIndex)
    {
      if (task is ReadTask read)
      {
        Read(drive, task.Sector);
      } 
      else if (task is WriteTask write)
      {
        Write(drive, write.Sector, write.Value);
      }
      _tasks.Remove(task);
    }
  }
}