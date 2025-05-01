using Silk.NET.Input;
using ImGuiNET;
using System.Numerics;
using Drvv.Model;

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
    if (Model.Algorithm.Running)
    {
      ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.3f, 0.1f, 0.1f, 1f));
      ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.5f, 0.2f, 0.2f, 1f));
      ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.25f, 0f, 0f, 1f));
      if(ImGui.Button("Abort", new(80, 40)))
      {
        Model.Algorithm.Running = false;
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
      Model.Algorithm.Running = true;
    }
    ImGui.PopStyleColor(3);
  }

  public void Apply()
  {
    ImGui.Begin("Visualisation");
    HugeStartButton();
    _tasks.Apply();
    ImGui.End();

    ImGui.Begin("Examples");

    if (ImGui.Button("Deep format"))
    {
      for (int i = 0; i < Model.Disk.Rows * Model.Disk.Columns; i++)
      {
        Model.Tasks.Add(new WriteTask() { Sector = i, Value = new Data(0) });
      }
    }
    if (ImGui.Button("Write gibberish"))
    {
      Random rng = new();
      for (int i = 0; i < Model.Disk.Rows * Model.Disk.Columns; i++)
      {
        Model.Tasks.Add(new WriteTask() { Sector = i, Value = new Data(rng.Next(8)) });
      }
    }
    if (!Model.Algorithm.Running && ImGui.Button("Shuffle"))
    {
      Random rng = new();
      for (int i = 0; i < Model.Tasks.Count; i++)
      {
        int pickedIndex = rng.Next(0, (int)Model.Disk.Rows * (int)Model.Disk.Columns);
        (Model.Tasks[i], Model.Tasks[pickedIndex]) = (Model.Tasks[pickedIndex], Model.Tasks[i]);
      }
    }

    ImGui.End();
  }
}
