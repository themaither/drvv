using System.Drawing;
using Drvv.Primitives;
using Silk.NET.OpenGL;

namespace Drvv.Renderer.For;

class App : IRendererFor<Model.App>
{
  private readonly IVertexRendererContext _vertexCtx;
  public Model.App Model { get; }

  public App(Model.App model, IVertexRendererContext vertexCtx, IGLRendererContext glCtx)
  {
    Model = model;
    _vertexCtx = vertexCtx;
  }

  public void Render()
  {
  }
}