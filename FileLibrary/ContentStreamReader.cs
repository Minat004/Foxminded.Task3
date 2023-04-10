namespace FileLibrary;

public class ContentStreamReader : IContentStreamReader
{
    private readonly string _path;
    
    public ContentStreamReader(string path)
    {
        _path = path;
    }

    public List<string?> ReadLines()
    {
        var lines = new List<string?>();
        
        try
        {
            using (var streamReader = new StreamReader(_path))
            {
                while (!streamReader.EndOfStream)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cant read the file: {_path}");
            Console.WriteLine(e.Message);
        }

        return lines;
    }
}