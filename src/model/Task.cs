namespace Drvv.Model;

abstract class Task
{
  public int Sector { get; set; }
  
}

class ReadTask : Task
{
}

class WriteTask : Task
{
  public Model.Data Value { get; set; }
}

