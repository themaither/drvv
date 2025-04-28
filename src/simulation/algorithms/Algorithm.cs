namespace Drvv.Simulation.Algorithms;

abstract class Algorithm
{
  public bool Running { get; set; }
  public void Update(float deltaTime)
  {
    if (!Running) return;
    OnUpdate(deltaTime);
  }
  protected abstract void OnUpdate(float deltaTime);
}