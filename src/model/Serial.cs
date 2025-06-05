using System.Text;

namespace Drvv.Model;

class Serial
{
  private List<string> _messages;

  public bool NewEntryAdded { get; set; }

  public Serial()
  {
    _messages = new();
  }

  public IEnumerable<string> Messages => _messages;

  public void Append(string message)
  {
    _messages.Add(message);
    NewEntryAdded = true;
  }

  public void Flush()
  {
    _messages.Clear();
  }
}

class SerialWriter
{
  public Serial Serial { get; set; }

  private string _prefix;

  public SerialWriter(Serial serial, string prefix)
  {
    Serial = serial;
    _prefix = prefix;
  }

  public void WriteLine(string content)
  {
    Serial.Append($"[{DateTime.Now.TimeOfDay}] [{_prefix}] {content}");
  }
}