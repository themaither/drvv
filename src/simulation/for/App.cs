namespace Drvv.Simulation.For;

class App
{
  public App(Model.App model)
  {
    Model = model;
    _drive = new(model.Drive);
  }

  public Model.App Model { get; }

  private Drive _drive;

  public void Update(float deltaTime)
  {
    _drive.Update(deltaTime);
    Model.Selection.SelectedIndex = Model.Disk.Head.TargetSector;
    Model.Algorithm.Update(deltaTime);
  }
}
