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

    //TODO: rewrite this abomination
    if (Model.Rotation < 0)
      Model.Rotation += MathF.PI * 2;
    if (Model.Rotation > MathF.PI * 2)
      Model.Rotation -= MathF.PI * 2;
  }  
}