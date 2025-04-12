using System.Numerics;
using ImGuiNET;
using Silk.NET.Maths;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace Drvv.UI.For;

class App
{
  public Model.App Model { get; }
  public App(Model.App model, ImGuiController context) 
  {
    Model = model;
  }

  public void Apply()
  {
    {
      int selection = Model.Selection.SelectedIndex;
      ImGui.InputInt("Selection", ref selection, 1, 5);
      Model.Selection.SelectedIndex = selection;
    }
    {
      Vector2 pos = Model.Disk.Pointer.Target.ToSystem();
      ImGui.DragFloat2("Pointer", ref pos, 0.01f, -2f, 2f);
      Model.Disk.Pointer.Target = new(pos.X, pos.Y);
    }

  }
}