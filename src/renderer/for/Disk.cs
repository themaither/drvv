using System.Numerics;
using Drvv.Model;
using Drvv.Primitives;
using Silk.NET.Maths;

namespace Drvv.Renderer.For;

class Disk : IRendererFor<Model.Disk>
{
  public Model.Disk Target { get; }
  public Selection Selection { get; set; }

  public Model.Disk Model => throw new NotImplementedException();

  private readonly IVertexRendererContext _vertexCtx;

  public Disk(Model.Disk target, Selection selection, IVertexRendererContext context)
  {
    Target = target;
    Selection = selection;
    _vertexCtx = context;
  }

  public void Render()
  {
    var pri = Primitives.Disk<Vector2D<float>>.Generate(0.1f, 0.6f, 16, 64, 12f);
    var color = new Color3<float>(0f, 0f, 0f);
    var selectedColor = new Color3<float>(.2f, .3f, .4f);
    _vertexCtx.CommitTriangles(
      pri
        .Lower()
        .SelectMany(a => a.Lower())
        .Index()
        .Select(
          a => a.Value.TransformType((b) => new Vertex(b, a.Index == Selection.SelectedIndex ? selectedColor : color))
        )
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Lower())
        .SelectMany(a => a.Dissolve())
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