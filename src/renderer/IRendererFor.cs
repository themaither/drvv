namespace Drvv.Renderer;
interface IRendererFor<T> : IRenderer
{
  public T Model { get; }
}
