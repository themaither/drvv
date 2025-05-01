using Drvv.Primitives;

namespace Drvv.Renderer.For;

class Data
{
  public Dictionary<int, Color3<float>> _colorOverrides;

  public Data()
  {
    _colorOverrides = new()
    {
      { 0, new(0, 0, 0) },
      { 1, new(1, 0, 0) },
      { 2, new(0, 1, 0) },
      { 3, new(0, 0, 1) },
      { 4, new(1, 1, 0) },
      { 5, new(0, 1, 1) },
      { 6, new(1, 0, 1) },
      { 7, new(0.5f, 0, 0) },
      { 8, new(0, 0.5f, 0) }
    };
  }

  public Color3<float> GetColorFor(Model.Data data)
  {
    if (!_colorOverrides.TryGetValue(data.Value, out Color3<float> value))
    {
      value = new Color3<float>(

            0.5f, 0.5f, 0.5f
        
      );
      _colorOverrides.Add(data.Value, value);
    } 
    return value;

  }
}
