using Silk.NET.Input;
using ImGuiNET;
using System.Numerics;

namespace Drvv.UI.For;

class App
{
  public Model.App Model { get; }
  private IInputContext _ctx;

  private Tasks _tasks;
  public App(Model.App model, IInputContext context) 
  {
    Model = model;
    _ctx = context;
    _tasks = new(Model.Tasks);
  }

  public void HugeStartButton()
  {
    if (Model.Disk.Running)
    {
      ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.3f, 0.1f, 0.1f, 1f));
      ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.5f, 0.2f, 0.2f, 1f));
      ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.25f, 0f, 0f, 1f));
      if(ImGui.Button("Abort", new(80, 40)))
      {
        Model.Disk.Running = false;
      }
      ImGui.PopStyleColor(3);
      return;
    }
    ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.1f, 0.3f, 0.1f, 1f));
    ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.2f, 0.5f, 0.2f, 1f));
    ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0f, 0.25f, 0f, 1f));
    if(ImGui.Button("Start", new(80, 40)))
    {
      Model.Disk.Running = true;
    }
    ImGui.PopStyleColor(3);
  }

  public void Apply()
  {
    ImGui.Begin("Visualisation");
    HugeStartButton();
    _tasks.Apply();
    ImGui.End();

    ImGui.ShowDemoWindow();
  }
}
