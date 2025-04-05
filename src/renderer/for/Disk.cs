namespace Drvv.Renderer.For;

class Disk : IRendererFor<Model.Disk>
{
  public Model.Disk Target { get; }

  public Model.Disk Model => throw new NotImplementedException();

  public Disk(Model.Disk target)
  {
    Target = target;
  }

  public void Render()
  {
    throw new NotImplementedException();
  }
}