
using Silk.NET.Maths;

namespace Drvv.Simulation.For;

class App
{
  public App(Model.App model)
  {
    Model = model;
  }

  public Model.App Model { get; }

  public void Update(float deltaTime)
  {
    float distance = Vector2D.Distance(Vector2D<float>.Zero, Model.Disk.Pointer.Target);
    float angle = MathF.Acos(
      Vector2D.Dot(
        Vector2D<float>.UnitY,
        Model.Disk.Pointer.Target
      ) / distance );

    if (Model.Disk.Pointer.Target.X < 0)
    {
      angle = MathF.PI - angle + MathF.PI;
    }

    float markedRow = (distance - Model.Disk.InnerRadius) / (Model.Disk.OuterRadius - Model.Disk.InnerRadius) * Model.Disk.Rows;
    float markedColumn = (angle / MathF.PI) * Model.Disk.Columns / 2;

    Model.Selection.SelectedIndex 
      = (int)markedColumn + (int)markedRow * (int)Model.Disk.Columns;

    if (distance < Model.Disk.InnerRadius)
    {
      Model.Selection.SelectedIndex = -1;
    }

    Console.WriteLine(markedRow);
  }
}