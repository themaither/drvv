using Drvv.Primitives;

namespace Drvv.Renderer;
interface IVertexRendererContext
{
  void CommitTriangles(IEnumerable<Vertex> vertices);
  void CommitLines(IEnumerable<Vertex> vertices);
}