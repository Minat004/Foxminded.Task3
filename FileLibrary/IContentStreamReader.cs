namespace FileLibrary;

public interface IContentStreamReader
{
    public IEnumerable<string?> ReadLines();
}