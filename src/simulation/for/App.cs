
using Silk.NET.Maths;

namespace Drvv.Simulation.For;

class App
{
  public App(Model.App model)
  {
    Model = model;
    _disk = new(model.Disk);
  }

  public Model.App Model { get; }

  private Disk _disk;

  public void Update(float deltaTime)
  {
    _disk.Update(deltaTime);
    Model.Selection.SelectedIndex = Model.Disk.Head.TargetSector;
    Model.Algorithm.Update(deltaTime);
  }
}
