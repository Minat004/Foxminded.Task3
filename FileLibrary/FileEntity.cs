namespace FileLibrary;

public class FileEntity
{
    public FileEntity(string path)
    {
        Path = path;
    }

    public string Path { get; }
}