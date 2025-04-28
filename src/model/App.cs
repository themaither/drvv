using Drvv.Simulation.Algorithms;
using Silk.NET.Maths;

namespace Drvv.Model;

class App
{
  public App()
  {
    Disk = new();
    Selection = new() {SelectedIndex = -1};
    Screen = new() { AspectRatio = .5f };
    Tasks = [];
    Algorithm = new FCFS(Tasks, Disk);
  }

  public Disk Disk { get; set; }

  public Selection Selection { get; set; }

  public Screen Screen { get; set; }

  public Vector2D<float> Pointer { get; set; }

  public List<Model.Task> Tasks { get; set; }

  public Algorithm Algorithm { get; set; }
}
