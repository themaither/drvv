using ImGuiNET;

namespace Drvv.UI.For;

class Drive
{
  public Drive(Model.Drive model)
  {
    Model = model;
  }

  public Model.Drive Model { get; }

  public event EventHandler? NewPressed;

  public void Apply()
  {
    ImGui.Begin("Drive");

    ImGui.Columns(1);

    var windowBounds = ImGui.GetWindowContentRegionMax() - ImGui.GetWindowContentRegionMin();

    if (ImGui.Button("New", new(windowBounds.X, 20)))
    {
      NewPressed?.Invoke(this, new());
    }

    {
      bool running = Model.Running;
      Global.SwitchButton(ref running, "Start", "Stop", new(windowBounds.X, 50));
      if (running)
      {
        Model.Start(); 
      } else {
        Model.Stop();
      }
    }

    {
      ImGui.LabelText(Model.Running ? "running" : "idle", "Status: ");
      ImGui.LabelText(Model.Disks.Length.ToString(), "Disks: ");
      ImGui.LabelText(Model.Rows.ToString(), "Tracks per disk: ");
      ImGui.LabelText((Model.Cylinders / Model.Rows).ToString(), "Sectors per track: ");
      ImGui.LabelText(Model.Cylinders.ToString(), "Cylinders: ");
    }


    ImGui.End();
  }
}