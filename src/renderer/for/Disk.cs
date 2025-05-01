using System.Numerics;
using Drvv.Model;
using Drvv.Primitives;
using Silk.NET.Maths;

namespace Drvv.Renderer.For;

class Disk
{
  public Model.Disk Model { get; }

  private readonly IVertexRendererContext _vertexCtx;
  private readonly Head _head;
  private readonly Data _data;

  public Disk(Model.Disk target, IVertexRendererContext context)
  {
    Model = target;
    _vertexCtx = context;
    _head = new(Model.Head, _vertexCtx);
    _data = new();
  }

  public void Render()
  {
    var pri = Primitives.Disk<Vector2D<float>>.Generate(
      Model.InnerRadius, Model.OuterRadius, Model.Rotation,
      Model.Rows, Model.Columns, 12f
    );
    var color = new Color3<float>(0f, 0f, 0f);
    var selectedColor = new Color3<float>(.2f, .3f, .4f);
    _vertexCtx.CommitTriangles(
      pri
        .Lower()
        .SelectMany(a => a.Lower())
        .Index()
        .Select(
         // a => a.Value.TransformType((b) => new Vertex(b, a.Index == Model.Head.PointingIndex ? selectedColor : color))
          a => a.Value.TransformType( (b) => new Vertex(b, _data.GetColorFor(Model.Data[a.Index])) )
        )
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Dissolve())
        .Select(a => new Vertex(a.Position + Model.Position, a.Color))
    );

    _vertexCtx.CommitLines(
      pri
        .Lower()
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Outline())
        .SelectMany(a => a.Dissolve())
        .Select(a => new Primitives.Vertex(a, new Color3<float>(1f, 1f, 1f)))
        .Select(a => new Vertex(a.Position + Model.Position, a.Color))
    );

    _head.Render();
  }
}
