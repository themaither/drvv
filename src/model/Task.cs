namespace Drvv.Model;

abstract class Task
{
  
}

class ReadTask : Task
{
  public int Sector { get; set; }
}

class WriteTask : Task
{
  public int Sector { get; set; }

  public Model.Data Value { get; set; }
}

