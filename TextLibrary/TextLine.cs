namespace TextLibrary;

public class TextLine
{
    private readonly string? _line;
    public TextLine(string? line)
    {
        _line = line;
    }

    public List<string> ArrayLine { get; private set; } = new();

    public void Separate(char separator = ',')
    {
        var sep = _line!.Split(separator);
        ArrayLine = new List<string>(sep.ToArray());
    }
}