namespace Drvv.Renderer.For;
class Data
{
  private Random _random;

  public Dictionary<int, Color3<float>> _colorOverrides;

  public Data(Random random)
  {
    _random = random;
    _colorOverrides = new();
    _colorOverrides.Add(0, new(0, 0, 0));
    _colorOverrides.Add(1, new(1, 0, 0));
    _colorOverrides.Add(2, new(0, 1, 0));
    _colorOverrides.Add(3, new(0, 0, 1));
    _colorOverrides.Add(4, new(1, 1, 0));
    _colorOverrides.Add(5, new(0, 1, 1));
    _colorOverrides.Add(6, new(1, 0, 1));
    _colorOverrides.Add(7, new(0.5f, 0, 0));
    _colorOverrides.Add(8, new(0, 0.5f, 0));
  }

  public Color3<float> GetColorFor(Model.Data data)
  {
    if (!_colorOverrides.ContainsKey(data.Value))
    {
      _colorOverrides.Add(data.Value, new Color3<float>(

            0.5f, 0.5f, 0.5f
        
      ));
    } 
    return _colorOverrides[data.Value];

  }
}
