namespace Drvv.Simulation.For;

class Drive
{
  public Drive(Model.Drive model)
  {
    Model = model;
    _diskSimulations = new(model.Disks.Length);
    foreach (var disk in Model.Disks)
    {
      _diskSimulations.Add(new Disk(disk));
    }
  }
  
  public Model.Drive Model { get; }

  private readonly List<Disk> _diskSimulations;

  public void Update(float deltaTime)
  {
    foreach (var diskSimulation in _diskSimulations)
    {
      diskSimulation.Update(deltaTime);
    }
  }
}
