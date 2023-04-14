namespace FileLibrary;

public class ContentStreamReader : IContentStreamReader
{
    private readonly string _path;
    
    public ContentStreamReader(string path)
    {
        _path = path;
    }

    public IEnumerable<string?> ReadLines()
    {
        using (var streamReader = new StreamReader(_path))
        {
            while (!streamReader.EndOfStream)
            {
                yield return streamReader.ReadLine();
            }
        }
    }
}