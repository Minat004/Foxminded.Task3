namespace FileLibrary;

public class ContentStreamReader : IContentStreamReader
{
    private readonly StreamReader? _streamReader;
    
    public ContentStreamReader(FileEntity file)
    {
        _streamReader = new StreamReader(file.Path);
    }

    public List<string?> ReadLines()
    {
        var lines = new List<string?>();
        
        try
        {
            while (!_streamReader!.EndOfStream)
            {
                lines.Add(_streamReader.ReadLine());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return lines;
    }

    public void Dispose()
    {
        _streamReader!.Close();
    }
}