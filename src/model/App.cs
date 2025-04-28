using Drvv.Simulation.Strategy;
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
    Strategy = new FIFOStrategy(Tasks, Disk);
  }

  public Disk Disk { get; set; }

  public Selection Selection { get; set; }

  public Screen Screen { get; set; }

  public Vector2D<float> Pointer { get; set; }

  public List<Model.Task> Tasks { get; set; }

  public Strategy Strategy { get; set; }
}
