using Silk.NET.Maths;

namespace Drvv.Simulation.For;

class Head
{
  public Head(Model.Head model)
  {
    Model = model;
  }
  
  public Model.Head Model { get; }

  public void Update(float deltaTime)
  {
    float distance = Vector2D.Distance(Vector2D<float>.Zero, Model.Owner.Head.Target);
    float angle = MathF.Acos(
      Vector2D.Dot(
        Vector2D<float>.UnitY,
        Model.Owner.Head.Target
      ) / distance );

    if (Model.Owner.Head.Target.X < 0)
    {
      angle = MathF.PI - angle + MathF.PI;
    }
    
    angle -= Model.Owner.Rotation;

    if (angle < 0)
    {
      angle += MathF.PI * 2;
    }

    float markedRow = 
      (distance - Model.Owner.InnerRadius) 
      / (Model.Owner.OuterRadius - Model.Owner.InnerRadius) * Model.Owner.Rows;
    float markedColumn = (angle / MathF.PI) * Model.Owner.Columns / 2;

    Model.PointingIndex 
      = (int)markedColumn + (int)markedRow * (int)Model.Owner.Columns;

    if (distance < Model.Owner.InnerRadius)
    {
      Model.PointingIndex = -1;
    }
  }  
}