namespace FileLibrary;

public interface IContentStreamReader : IDisposable
{
    public List<string?> ReadLines();
}