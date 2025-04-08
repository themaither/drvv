namespace Drvv.Model;

class App
{
  public App()
  {
    Disk = new();
    Selection = new() {SelectedIndex = -1};
    Screen = new() { AspectRatio = .5f };
  }

  public Disk Disk { get; set; }

  public Selection Selection { get; set; }

  public Screen Screen { get; set; }
}