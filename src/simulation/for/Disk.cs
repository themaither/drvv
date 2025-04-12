using Silk.NET.Maths;

namespace Drvv.Simulation.For;

class Disk
{
  public Disk(Model.Disk model)
  {
    Model = model;
    _head = new(model.Head);
  }
  
  public Model.Disk Model { get; }

  private Head _head;

  public void Update(float deltaTime)
  {
    _head.Update(deltaTime);
  }  
}