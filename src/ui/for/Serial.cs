using Drvv.Model;
using ImGuiNET;

namespace Drvv.UI.For;

class Serial
{
  public Model.Serial Model { get; set; }

  public bool _autoscroll;

  public Serial(Model.Serial model)
  {
    Model = model;
    _autoscroll = true;
  }

  public void Apply()
  {
    ImGui.Begin("Serial");

    if(ImGui.Button("Flush"))
    {
      Model.Flush();
    }
    ImGui.SameLine();
    ImGui.Checkbox("Autoscroll", ref _autoscroll);

    ImGui.BeginChild("messages", new(0, 0), ImGuiChildFlags.Border);

    foreach (var message in Model.Messages)
    {
      ImGui.Text(message);
    }

    if (Model.NewEntryAdded)
    {
      if (_autoscroll)
      {
        ImGui.SetScrollHereY(1.0f);
      }
      Model.NewEntryAdded = false;
    }

    ImGui.EndChild();

    ImGui.End();
  }
}