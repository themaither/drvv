using System.Reflection;
using ImGuiNET;

namespace Drvv.UI.For;

class Algorithm
{
  public Algorithm(Model.App model)
  {
    Model = model;
  }

  public Model.App Model { get; } 

  public void Apply()
  {
    ImGui.Begin("Algorithm");

    if (Model.Algorithm.Running)
      ImGui.BeginDisabled();

    int selectedIndex = Model.AlgorithmSelectedIndex;
    string[] list = Model.Algorithms.Select(a => a.GetType().GetCustomAttribute<AlgorithmInfoAttribute>()!.Name).ToArray();
    ImGui.ListBox("", ref selectedIndex, list, list.Length);
    Model.AlgorithmSelectedIndex = selectedIndex;

    ImGui.TextWrapped(Model.Algorithm.GetType().GetCustomAttribute<AlgorithmInfoAttribute>()!.Description);

    if (Model.Algorithm.Running)
      ImGui.EndDisabled();

    ImGui.End();
  }
}