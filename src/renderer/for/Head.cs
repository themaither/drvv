
using Silk.NET.Maths;

namespace Drvv.Renderer.For;

class Head : IRendererFor<Model.Head>
{
  public Model.Head Model { get; }

  private readonly IVertexRendererContext _vertexCtx;

  public Head(Model.Head model, IVertexRendererContext vertexCtx)
  {
    Model = model;
    _vertexCtx = vertexCtx;
  }

  public void Render()
  {
    _vertexCtx.CommitTriangles(
      Primitives.Triangle<Vector2D<float>>.GenerateDirectional(new(.5f, -.5f), Model.Target, .2f)
      .Dissolve()
      .Select(a => new Primitives.Vertex(a, new(0f, 1f, 0f)))
    );
  }
}
