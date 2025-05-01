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
    Model.Rotation -= deltaTime * MathF.PI * Model.Speed;

    if (Model.Running)
    {
      if (Model.Speed < 1)
      Model.Speed += 0.1f * deltaTime;
    }
    else 
    {
      if (Model.Speed > 0)
      Model.Speed -= 0.1f * deltaTime;
    }
  }
}
