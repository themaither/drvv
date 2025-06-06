namespace Drvv;

class Box<T>
{
  public Box(T value)
  {
    Value = value;
  }

  public T Value { get; set; }
}