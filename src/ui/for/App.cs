using Silk.NET.Input;
using ImGuiNET;
using System.Numerics;
using Drvv.Model;
using Silk.NET.OpenGL.Extensions.ImGui;

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
        foreach (var item in Model.Drive.Disks)
        {
          item.Running = false;
        }
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

  float _mouseX, _mouseY;

  public void ApplyCamera() {
    float currentMouseX = _ctx.Mice[0].Position.X;
    float currentMouseY = _ctx.Mice[0].Position.Y;

    if (_ctx.Mice[0].IsButtonPressed(MouseButton.Left))
    {
      Model.Screen.CameraPosition += new Silk.NET.Maths.Vector2D<float>(
        (currentMouseX - _mouseX) / Model.Screen.Resolution.X * 2,
        (_mouseY - currentMouseY) / Model.Screen.Resolution.Y * 2
      );
    }


    _mouseX = currentMouseX;
    _mouseY = currentMouseY;
  }

  public void Apply()
  {
    if (!ImGui.GetIO().WantCaptureMouse)
    {
      ApplyCamera();
    }
    ImGui.Begin("Visualisation");
    HugeStartButton();
    _tasks.Apply();
    ImGui.End();

    ImGui.Begin("Examples");

    if (ImGui.Button("Deep format"))
    {
      for (int i = 0; i < Model.Drive.Cylinders * Model.Drive.Disks.Length; i++)
      {
        Model.Tasks.Add(new WriteTask() { Sector = i, Value = new Data(0) });
      }
    }
    if (ImGui.Button("Write gibberish"))
    {
      Random rng = new();
      for (int i = 0; i < Model.Drive.Cylinders * Model.Drive.Disks.Length; i++)
      {
        Model.Tasks.Add(new WriteTask() { Sector = i, Value = new Data(rng.Next(8)) });
      }
    }
    if (ImGui.Button("FCFS Test case"))
    {
      for (int i = 0; i < Model.Drive.Disks.First().Columns; i++)
      {
        Model.Tasks.Add(new WriteTask() { Sector = i, Value = new Data(2) });
        Model.Tasks.Add(new WriteTask() { Sector = i + Model.Drive.Disks.First().Columns * 4, Value = new Data(3) });
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
    if (!Model.Algorithm.Running && ImGui.Button("Stable shuffle"))
    {
      //TODO:
    }

    ImGui.End();
  }
}
