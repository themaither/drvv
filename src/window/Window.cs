using Drvv.Model;
using Drvv.Renderer;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;

namespace Drvv.Window;

class Window
{
  private readonly Silk.NET.Windowing.IWindow _handle;

  public App Model { get; private set; }
  public Context RenderContext { get; private set; }
  public Renderer.For.App Renderer { get; private set; }
  public ImGuiController UIContext { get; private set; }
  public UI.For.App UI { get; private set; }
  public GL GL { get; set; }
  public Simulation.For.App Simulation { get; private set; }

  public IInputContext InputContext { get; private set; }

  #pragma warning disable CS8618
  public Window(App model)
  {
    _handle = Silk.NET.Windowing.Window.Create(WindowOptions.Default);
    _handle.Load += () => {
      GL = _handle.CreateOpenGL();
      RenderContext = new(Model!.Screen, GL);
      InputContext = _handle.CreateInput();
      Renderer = new Renderer.For.App(Model!, RenderContext, RenderContext);
      UIContext = new(RenderContext.GL, _handle, InputContext);
      UI = new(Model!, InputContext);
      Simulation = new(Model!);
      OnFramebufferResize(_handle.Size);
    };
    Model = model;
    _handle.Render += (double deltaTime) => {
      RenderContext!.DeltaTime = deltaTime;
      Renderer!.Render();
      RenderContext!.Render();
      UIContext!.Render();
    };
    _handle.Update += (double deltaTime) => {
      UIContext!.Update((float)deltaTime);
      UI!.Apply();
      Simulation!.Update((float)deltaTime);
    };
    _handle.FramebufferResize += OnFramebufferResize;
    
    model.ModelChangeRequested += (model) => {
      ReplaceModel(model);
    };

    _handle.Run();
  }
  #pragma warning restore CS8618

  private void OnFramebufferResize(Vector2D<int> size)
  {
    Model!.Screen.AspectRatio = (float)size.X / size.Y;
    Model!.Screen.Resolution = size;
    GL!.Viewport(size);
  }

  public void ReplaceModel(Model.App model) {
    model.Screen = Model.Screen;
    model.ModelChangeRequested += (model) => ReplaceModel(model);
    Model = model;
    Renderer = new(model, RenderContext, RenderContext);
    UI = new(model, InputContext);  
    Simulation = new(model);
  }
}