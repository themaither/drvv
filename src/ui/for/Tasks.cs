using Drvv.Simulation.Algorithms;
using ImGuiNET;

namespace Drvv.UI.For;

class Tasks
{
  public List<Model.Task> Model { get; }

  public Tasks(List<Model.Task> model, Simulation.Algorithms.Algorithm algorithm)
  {
    Model = model;
    _algorithm = algorithm;
  }

  public event Action? ShowExamples;

  private Simulation.Algorithms.Algorithm _algorithm;

  public void Apply()
  {
    ImGui.Begin("Tasks");

    var windowBounds = ImGui.GetWindowContentRegionMax() - ImGui.GetWindowContentRegionMin();

    ImGui.AlignTextToFramePadding();
    if (ImGui.Button("Clear"))
    {
      Model.Clear();
    }
    ImGui.SameLine();
    if (ImGui.Button("Show Presets"))
    {
      ShowExamples?.Invoke();
    }

    if (ImGui.Button("Add READ"))
    {
      Model.Add(new Model.ReadTask());
    }
    ImGui.SameLine();
    if (ImGui.Button("Add WRITE"))
    {
      Model.Add(new Model.WriteTask());
    }

    {
      bool running = _algorithm.Running;
      ImGui.SetCursorPos(new(160, 25));
      Global.SwitchButton(ref running, "Execute", "Abort", new(windowBounds.X - 152, 45));
      _algorithm.Running = running;
    }

    ImGui.BeginChild("list", new(0, 0), ImGuiChildFlags.Border);
    for (int i = 0; i < Model.Count; i++)
    {
      ImGui.PushID(i);
      //TODO: put this somewhere else
      if (ImGui.Button("X"))
      {
        Model.RemoveAt(i);
        break;
      }
      ImGui.SameLine();
      if (Model[i] is Model.ReadTask read)
      {
        ImGui.BeginChild("", new(0, 60), ImGuiChildFlags.Border);
        ImGui.AlignTextToFramePadding();
        ImGui.Text("READ");

        int sector = read.Sector;
        ImGui.Text("Sector:");
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("", ref sector);
        if (sector < 0) sector = 0;
        read.Sector = sector;
        
        ImGui.SameLine();
      }
      if (Model[i] is Model.WriteTask write)
      {
        ImGui.BeginChild("", new(0, 85), ImGuiChildFlags.Border);
        ImGui.AlignTextToFramePadding();
        ImGui.Text("WRITE");

        int sector = write.Sector;
        ImGui.Text("Sector:");
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("", ref sector);
        if (sector < 0) sector = 0;
        write.Sector = sector;

        ImGui.PushID("1");
        int value = write.Value.Value;
        ImGui.Text("Value:");
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("", ref value);
        if (value < 0) value = 0;
        write.Value = new(value);
        ImGui.PopID();
      }
      ImGui.EndChild();
      ImGui.PopID();
    }
    ImGui.EndChild();

    ImGui.End();
  }
}
