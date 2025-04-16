using System.Drawing;
using Drvv.Model;
using Drvv.Primitives;
using Silk.NET.OpenGL;

namespace Drvv.Renderer.For;

class App : IRendererFor<Model.App>
{
  private readonly IVertexRendererContext _vertexCtx;
  private readonly Renderer.For.Disk _diskRenderer;
  public Model.App Model { get; }
  public App(Model.App model, IVertexRendererContext vertexCtx, IGLRendererContext glCtx)
  {
    Model = model;
    _vertexCtx = vertexCtx;
    _diskRenderer = new(Model.Disk, _vertexCtx);
  }

  public void Render()
  {
    _diskRenderer.Render();
  }
}
