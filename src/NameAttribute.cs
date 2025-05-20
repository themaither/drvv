[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class NameAttribute : System.Attribute
{
  // See the attribute guidelines at
  //  http://go.microsoft.com/fwlink/?LinkId=85236

  public string Name { get; }

  // This is a positional argument
  public NameAttribute(string name)
  {
    Name = name;
  }
  
}