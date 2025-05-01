using Silk.NET.Maths;

namespace Drvv.Renderer.For;

class Head
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
      Primitives.Triangle<Vector2D<float>>.GenerateDirectional(
        new(-.25f * Model.Owner.Scale, -1f * Model.Owner.Scale), Model.Target, .2f * Model.Owner.Scale
      )
      .Dissolve()
      .Select(a => new Primitives.Vertex(a, new(0f, 1f, 0f)))
      .Select(a => new Primitives.Vertex(a.Position + Model.Owner.Position, a.Color))
    );
  }
}
