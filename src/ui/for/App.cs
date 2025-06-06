using Silk.NET.Input;
using ImGuiNET;
using System.Numerics;
using Drvv.Model;
using Silk.NET.OpenGL.Extensions.ImGui;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Drvv.UI.For;

class App
{
  public Model.App Model { get; }
  private readonly IInputContext _ctx;
  private readonly Tasks _tasks;
  private readonly NewDriveDialog _newDialog;
  private readonly Drive _drive;
  private readonly Algorithm _algorithm;
  private readonly Presets _presets;
  private readonly Serial _serial;
  float _mouseX, _mouseY;
  public App(Model.App model, IInputContext context) 
  {
    Model = model;
    _ctx = context;
    _newDialog = new(Model);
    _drive = new(Model.Drive);
    _drive.NewPressed += (o, e) => _newDialog.Shown = true;
    _algorithm = new(Model);
    _presets = new(Model);
    _tasks = new(Model.Tasks, model.BoxedAlgorithm);
    _tasks.ShowExamples += () => _presets.Shown = true;
    _serial = new(model.Serial);
  }

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

    Model.Screen.Scale += _ctx.Mice[0].ScrollWheels[0].Y / 20;

    _mouseX = currentMouseX;
    _mouseY = currentMouseY;
  }
 
  public void Apply()
  {
    if (!ImGui.GetIO().WantCaptureMouse)
    {
      ApplyCamera();
    }

    if (!_newDialog.Shown)
    {
      _drive.Apply();
      _algorithm.Apply();
      _tasks.Apply();
      _presets.Apply();
      _serial.Apply();
    }
    if (_newDialog.Shown)
    {
      _newDialog.Apply();
    }
  }
}
