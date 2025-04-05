namespace Drvv.Window;
using Drvv.Model;
using Drvv.Renderer;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;

class Window
{
  private readonly Silk.NET.Windowing.IWindow _handle;

  public App Model { get; }
  public Context RenderContext { get; private set; }
  public Renderer.For.App Renderer { get; private set; }
  public ImGuiController UIContext { get; private set; }
  public UI.For.App UI { get; private set; }

  #pragma warning disable CS8618
  public Window(App model)
  {
    _handle = Silk.NET.Windowing.Window.Create(WindowOptions.Default);
    _handle.Load += () => {
      RenderContext = new(_handle.CreateOpenGL());
      Renderer = new Renderer.For.App(Model!, RenderContext, RenderContext);
      UIContext = new(RenderContext.GL, _handle, _handle.CreateInput());
      UI = new(Model!, UIContext);
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
    };
    _handle.Run();
  }
  #pragma warning restore CS8618
}