namespace Drvv.Model;

class App
{
  public App()
  {
    Disk = new();
    Selection = new() {SelectedIndex = -1};
  }

  public Disk Disk { get; set; }

  public Selection Selection { get; set; }
}