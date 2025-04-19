namespace Drvv.UI.For;
using ImGuiNET;

class Tasks
{
  List<Model.Task> Model;

  public Tasks(List<Model.Task> model)
  {
    Model = model;
  }

  public void Apply()
  {
    ImGui.AlignTextToFramePadding();
    ImGui.Text("Tasks");
    ImGui.SameLine();
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
      ImGui.BeginChild("", new(0, 35), ImGuiChildFlags.Border);
      //TODO: put this somewhere else
      if (ImGui.Button("X"))
      {
        Model.RemoveAt(i);
        break;
      }
      ImGui.SameLine();
      if (Model[i] is Model.ReadTask read)
      {
        ImGui.AlignTextToFramePadding();
        ImGui.Text("READ");

        int sector = read.Sector;
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("Sector", ref sector);
        read.Sector = sector;
        
        ImGui.SameLine();
        if (ImGui.Button("X"))
        {
          Model.RemoveAt(i);
          break;
        }
      }
      if (Model[i] is Model.WriteTask write)
      {
        ImGui.AlignTextToFramePadding();
        ImGui.Text("WRITE");

        int sector = write.Sector;
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("Sector", ref sector);
        write.Sector = sector;

        int value = write.Value.Value;
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("Value", ref value);
        write.Value = new(value);

      }
      ImGui.EndChild();
      ImGui.PopID();
    }
    ImGui.EndChild();
  }
}
