using System.Numerics;
using ImGuiNET;

namespace Drvv.UI
{
  static class Global
  {
    public static void SwitchButton(ref bool state, string textOn, string textOff, Vector2 size) 
    {
      if (state)
      {
        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.3f, 0.1f, 0.1f, 1f));
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.5f, 0.2f, 0.2f, 1f));
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.25f, 0f, 0f, 1f));
        if(ImGui.Button(textOff, size))
        {
          state = false;
        }
        ImGui.PopStyleColor(3);
        return;
      }
      ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.1f, 0.3f, 0.1f, 1f));
      ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.2f, 0.5f, 0.2f, 1f));
      ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0f, 0.25f, 0f, 1f));
      if(ImGui.Button(textOn, size))
      {
        state = true;
      }
      ImGui.PopStyleColor(3);
    }
  } 
}