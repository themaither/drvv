using System.Drawing;
using Drvv.Model;
using Drvv.Primitives;
using Silk.NET.OpenGL;

namespace Drvv.Renderer.For;

class App
{
  private readonly IVertexRendererContext _vertexCtx;
  private readonly Drive _driveRenderer;
  public Model.App Model { get; }
  public App(Model.App model, IVertexRendererContext vertexCtx, IGLRendererContext glCtx)
  {
    Model = model;
    _vertexCtx = vertexCtx;
    _driveRenderer = new(Model.Drive, _vertexCtx);
  }

  public void Render()
  {
    _driveRenderer.Render();
  }
}
