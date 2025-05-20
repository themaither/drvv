using ImGuiNET;

namespace Drvv.UI.For;

class Settings
{
  private bool _shown = false;

  private int _tracks = 2, _sectors = 4, _disks = 1;

  public Settings(Model.App model)
  {
    Model = model;
  }

  public bool Shown { get => _shown; set => _shown = value; }

  public Model.App Model { get; }

  public static Model.Drive CreateDrive(int disks, int rows, int columns) {
    Model.Drive drive = new(disks, rows, columns);

    for (int i = 0; i < disks; i++)
    {
      drive.Disks[i].Position = new(i * 1.5f, 0);
    }

    return drive;
  }

  public void Apply()
  {
    ImGui.Begin("Settings", ref _shown);

    {
      ImGui.InputInt("Tracks", ref _tracks);
      ImGui.InputInt("Sectors", ref _sectors);
      ImGui.InputInt("Disks", ref _disks);
      if (_tracks < 1) _tracks = 1; 
      if (_sectors < 1) _sectors = 1; 
      if (_disks < 0) _disks = 1; 
      if(ImGui.Button("Create hard drive")) {
        Model.ChangeModel(new Model.App(CreateDrive(_disks, _tracks, _sectors)));
      }
    }

    ImGui.End();
  }

}