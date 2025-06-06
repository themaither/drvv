using Drvv.Model;
using ImGuiNET;

namespace Drvv.UI.For;

class Presets
{
  public Presets(Model.App model)
  {
    Model = model;
  }

  public Model.App Model { get; }

  public bool Shown { get; set; }

  public void Apply()
  {
    if (!Shown)
      return;

    {
      bool shown = Shown;
      ImGui.Begin("Presets", ref shown);
      Shown = shown;
    }

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


    if (Model.Algorithm.Running)
      ImGui.BeginDisabled();

    if (ImGui.Button("Shuffle"))
    {
      Random rng = new();
      for (int i = 0; i < Model.Tasks.Count; i++)
      {
        int pickedIndex = rng.Next(0, Model.Tasks.Count);
        (Model.Tasks[i], Model.Tasks[pickedIndex]) = (Model.Tasks[pickedIndex], Model.Tasks[i]);
      }
    }

    if (Model.Algorithm.Running)
      ImGui.EndDisabled();

    ImGui.End();
  }
}