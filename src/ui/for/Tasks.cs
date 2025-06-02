using ImGuiNET;

namespace Drvv.UI.For;

class Tasks
{
  List<Model.Task> Model;

  public Tasks(List<Model.Task> model)
  {
    Model = model;
  }

  public event EventHandler? ShowExamples;

  public void Apply()
  {
    ImGui.Begin("Tasks");

    ImGui.AlignTextToFramePadding();
    if (ImGui.Button("Clear"))
    {
      Model.Clear();
    }
    ImGui.SameLine();
    if (ImGui.Button("Show Presets"))
    {
      ShowExamples?.Invoke(this, new());
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
