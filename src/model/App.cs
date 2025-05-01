using Drvv.Simulation.Algorithms;
using Silk.NET.Maths;

namespace Drvv.Model;

class App
{
  public App()
  {
    Drive = new(2, 16, 32);
    Drive.Disks[1].Position = new(2, 0);
    Selection = new() {SelectedIndex = -1};
    Screen = new() { AspectRatio = .5f };
    Tasks = [];
    Algorithm = new FCFS(Tasks, Disk);
  }

  public Drive Drive { get; set; }

  //TODO: Temporary, for compatibility reasons
  public Disk Disk => Drive.Disks[0];

  public Selection Selection { get; set; }

  public Screen Screen { get; set; }

  public Vector2D<float> Pointer { get; set; }

  public List<Model.Task> Tasks { get; set; }

  public Algorithm Algorithm { get; set; }
}
