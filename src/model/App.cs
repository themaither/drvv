using Drvv.Simulation.Algorithms;
using Silk.NET.Maths;

namespace Drvv.Model;

class App
{
  public App()
  {
    Drive = new(3, 4, 8);
    Drive.Disks[1].Position = new(1.5f, 0);
    Drive.Disks[2].Position = new(3f, 0);
    Selection = new() {SelectedIndex = -1};
    Screen = new() { AspectRatio = .5f };
    Tasks = [];
    Algorithms = [
      new FCFS(Tasks, Drive),
      new SSTF(Tasks, Drive) { ColumnBias = 1, RowBias = 40 }
    ];
  }

  public Drive Drive { get; set; }

  //TODO: Temporary, for compatibility reasons
  public Disk Disk => Drive.Disks[0];

  public Selection Selection { get; set; }

  public Screen Screen { get; set; }

  public Vector2D<float> Pointer { get; set; }

  public List<Model.Task> Tasks { get; set; }

  public Algorithm Algorithm => Algorithms[AlgorithmSelectedIndex];

  public Algorithm[] Algorithms { get; }

  public int AlgorithmSelectedIndex { get; set; }
}
