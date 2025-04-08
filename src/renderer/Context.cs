using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Drvv.Model;
using Drvv.Primitives;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace Drvv.Renderer;

class Context : IGLRendererContext, IVertexRendererContext
{
  private readonly Model.Screen _screen;
  private readonly List<Vertex> _triangles;
  private readonly List<Vertex> _lines;
  private uint _buffer;
  private uint _vertexArray;
  private uint _vertexShader;
  private uint _fragmentShader;
  private uint _program;
  private static string _vertexShaderCode = """
  #version 330 core
  layout (location = 0) in vec2 aPos;
  layout (location = 1) in vec3 aColor;
  out vec3 vsColor;
  uniform mat4 aspectRatio;
  void main()
  {
    vsColor = vec3(1.0, 1.0, 0.0);
    vsColor = aColor;
    gl_Position = aspectRatio * vec4(aPos.x, aPos.y, 0.0, 1.0);
  }
  """;

  private static string _fragmentShaderCode = """
  #version 330 core
  in vec3 vsColor;
  out vec4 FragColor;
  void main()
  {
    FragColor = vec4(vsColor, 1.0f);
  }
  """;

  public Context(Model.Screen screen, GL gl)
  {
    _screen = screen;
    GL = gl;
    _triangles = [];
    _lines = [];
    InitializeGL();
  }

  public GL GL { get; }
  public double DeltaTime { get; set; }

  public void CommitLines(IEnumerable<Vertex> vertices)
  {
    _lines.AddRange(vertices);
  }

  public void CommitTriangles(IEnumerable<Vertex> vertices)
  {
    _triangles.AddRange(vertices);
  }

  private void InitializeGL()
  {
    _buffer = GL.GenBuffer();
    GL.BindBuffer(GLEnum.ArrayBuffer, _buffer);
    _vertexArray = GL.GenVertexArray();
    GL.BindVertexArray(_vertexArray);
    GL.BindVertexBuffer(0, _buffer, 0, 0);
    GL.VertexAttribPointer(
      0, 2, GLEnum.Float, false,
      (uint)Marshal.SizeOf<Vertex>(),
      0
    );
    GL.VertexAttribPointer(
      1, 3, GLEnum.Float, false,
      (uint)Marshal.SizeOf<Vertex>(),
      sizeof(float) * 2
    );
    GL.EnableVertexAttribArray(0);
    GL.EnableVertexAttribArray(1);

    _vertexShader = GL.CreateShader(GLEnum.VertexShader);
    GL.ShaderSource(_vertexShader, _vertexShaderCode);
    GL.CompileShader(_vertexShader);
    _fragmentShader = GL.CreateShader(GLEnum.FragmentShader);
    GL.ShaderSource(_fragmentShader, _fragmentShaderCode);
    GL.CompileShader(_fragmentShader);
    _program = GL.CreateProgram();
    GL.AttachShader(_program, _vertexShader);
    GL.AttachShader(_program, _fragmentShader);
    GL.LinkProgram(_program);
  }

  public void Render()
  {
    GL.Clear((uint)GLEnum.ColorBufferBit);
    GL.ClearColor(Color.FromArgb(255, (int) (.25f * 255), (int) (.25f * 255), (int) (.30f * 255)));
    GL.UseProgram(_program);
    GL.BindVertexArray(_vertexArray);
    GL.BindBuffer(GLEnum.ArrayBuffer, _buffer);
    var matUniform = GL.GetUniformLocation(_program, "aspectRatio");
    GL.UniformMatrix4(matUniform, 1, false, _screen.ToMatrixArray());
    GL.BufferData<Vertex>(
      GLEnum.ArrayBuffer, 
      CollectionsMarshal.AsSpan(_triangles), GLEnum.StaticDraw
    );
    GL.DrawArrays(GLEnum.Triangles, 0, (uint)_triangles.Count);
    GL.BufferData<Vertex>(
      GLEnum.ArrayBuffer, 
      CollectionsMarshal.AsSpan(_lines), GLEnum.StaticDraw
    );
    GL.DrawArrays(GLEnum.Lines, 0, (uint)_lines.Count);
    _triangles.Clear();
    _lines.Clear();
  }
}