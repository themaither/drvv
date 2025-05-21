[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class AlgorithmInfoAttribute : System.Attribute
{
  // See the attribute guidelines at
  //  http://go.microsoft.com/fwlink/?LinkId=85236

  public string Name { get; }

  public string Description;

  // This is a positional argument
  public AlgorithmInfoAttribute(string name)
  {
    Name = name;
    Description = "";
  }
  
}