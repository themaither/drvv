using System.Numerics;
using Silk.NET.Maths;

namespace Drvv.Renderer.For;

class Disk : IRendererFor<Model.Disk>
{
  public Model.Disk Target { get; }

  public Model.Disk Model => throw new NotImplementedException();

  private readonly IVertexRendererContext _vertexCtx;

  public Disk(Model.Disk target, IVertexRendererContext context)
  {
    Target = target;
    _vertexCtx = context;
  }

  public void Render()
  {
    // var pri = Primitives.QuadStrip<Vector2D<float>>.GenerateArc(0f, 3f, 0.5f, 0.6f, 12f);
    var pri = Primitives.Disk<Vector2D<float>>.Generate(0.1f, 0.6f, 4, 12, 12f);

    _vertexCtx.CommitTriangles(
      pri
        .Lower()
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Dissolve())
        .Select(a => new Primitives.Vertex(a, new Color3<float>(.3f, 0f, 1f)))
    );

    _vertexCtx.CommitLines(
      pri
        .Lower()
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Outline())
        .SelectMany(a => a.Dissolve())
        .Select(a => new Primitives.Vertex(a, new Color3<float>(1f, 1f, 1f)))
    );
  }
}