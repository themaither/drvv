using System.Runtime.CompilerServices;
using Drvv.Simulation.Algorithms;
using Silk.NET.Maths;

namespace Drvv.Model;

class App
{
  private SerialWriter _algorithmWriter;

  public App() : this(new(1, 8, 16)) {}
  public App(Drive drive)
  {
    Drive = drive;
    Selection = new() {SelectedIndex = -1};
    Screen = new() { AspectRatio = .5f };
    Tasks = [];
    Algorithms = [
      new FCFS(Tasks, Drive),
      new SSTF(Tasks, Drive) { ColumnBias = 1, RowBias = 80 },
      new SCAN(Tasks, Drive),
      new CSCAN(Tasks, Drive),
      new LOOK(Tasks, Drive),
      new CLOOK(Tasks, Drive),
      new RA(Tasks, Drive)
    ];
    BoxedAlgorithm = new(Algorithm);
    Serial = new();
    _algorithmWriter = new(Serial, "drive");
    foreach (var algorithm in Algorithms)
    {
      algorithm.Log += (s) => _algorithmWriter.WriteLine(s);
    }
  }

  public Drive Drive { get; set; }

  //TODO: Temporary, for compatibility reasons
  public Disk Disk => Drive.Disks[0];

  public Selection Selection { get; set; }

  public Screen Screen { get; set; }

  public Vector2D<float> Pointer { get; set; }

  public List<Model.Task> Tasks { get; set; }

  public Algorithm Algorithm => Algorithms[AlgorithmSelectedIndex];

  public Box<Algorithm> BoxedAlgorithm { get; }

  public Algorithm[] Algorithms { get; }

  private int _algorithmSelectedIndex;

  public int AlgorithmSelectedIndex
  {
    get => _algorithmSelectedIndex;
    set 
    {
      _algorithmSelectedIndex = value;
      BoxedAlgorithm.Value = Algorithm;
    }
  }

  public event Action<App>? ModelChangeRequested;

  public Serial Serial { get; set; }

  public void ChangeModel(App model) {
    ModelChangeRequested?.Invoke(model);
  }
}
