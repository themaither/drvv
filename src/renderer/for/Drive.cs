using Drvv.Primitives;

namespace Drvv.Renderer.For;

class Drive
{
  private readonly IVertexRendererContext _vertexCtx;

  public Model.Drive Model { get; }

  private readonly List<Disk> _diskRenderers;

  public Drive(Model.Drive model, IVertexRendererContext context)
  {
    Model = model;
    _vertexCtx = context;
    _diskRenderers = new(model.Disks.Length);
    foreach (var disk in Model.Disks)
    {
      _diskRenderers.Add(new Disk(disk, context));
    }
  }

  public void Render()
  {
    foreach (var diskRenderer in _diskRenderers)
    {
      diskRenderer.Render();
    }
  }
}
