using ImGuiNET;
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
  }
}