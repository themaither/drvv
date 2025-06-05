using Drvv.Model;

namespace Drvv.Simulation.Algorithms;

class FCFSOnDisk : Algorithm
{
  private int _diskIndex;

  private Drive _drive;

  private Algorithm _parent;

  private int _offset;

  public FCFSOnDisk(List<Model.Task> tasks, int diskIndex, Drive drive, Algorithm parent, int offset)
    : base(tasks)
  {
    _tasks = tasks;
    _diskIndex = diskIndex;
    _drive = drive;
    _parent = parent;
    _offset = offset;
  }

  protected override void OnUpdate(float deltaTime)
  {
    _drive.Disks[_diskIndex].Running = true;
    if (_tasks.Count == 0) 
    {
      // _disk.Head.TargetRow = -1;
      Running = false;
      return;
    }

    if (_drive.Disks[_diskIndex].Speed <= 0.9f)
      return;

    _drive.Disks[_diskIndex].Head.TargetRow = ((_tasks.First().Sector) % (_drive.Disks[_diskIndex].Rows * _drive.Disks[_diskIndex].Columns)) / (int)_drive.Disks[_diskIndex].Columns;
    if (_drive.Disks[_diskIndex].Head.TargetSector == _tasks.First().Sector - _offset)
    {
      if (_tasks.First() is ReadTask read)
      {
        _parent.Read(_drive, _drive.Disks[_diskIndex].Head.TargetSector + _drive.Cylinders * _diskIndex);
      } 
      else if (_tasks.First() is WriteTask write)
      {
        _parent.Write(_drive, _drive.Disks[_diskIndex].Head.TargetSector + _drive.Cylinders * _diskIndex, write.Value);
      }
      _tasks.Remove(_tasks.First());
    }

  }
}