using System.Numerics;
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace Drvv.UI.For;

class App
{
  public Model.App Model { get; }
  private IInputContext _ctx;
  public App(Model.App model, IInputContext context) 
  {
    Model = model;
    _ctx = context;
  }

  public void Apply()
  {
    {
      Vector2 pos = Model.Disk.Head.Target.ToSystem();
      pos = Model.Screen.ScreenToWorld(new Vector2D<float>(_ctx.Mice[0].Position.X, _ctx.Mice[0].Position.Y)).ToSystem();
      Model.Pointer = new(pos.X, pos.Y);
    }
    {
      int selection = Model.Selection.SelectedIndex;
      ImGui.InputInt("Selection", ref selection, 1, 5);
      Model.Selection.SelectedIndex = selection;
    }
    {
      float rotation = Model.Disk.Rotation;
      ImGui.DragFloat("Rotation", ref rotation, 0.1f);
      Model.Disk.Rotation = rotation;
    }
    
  }
}