using System.Globalization;
using FileLibrary;

namespace MaxSum;

public static class Program
{
    public static void Main(string[] args)
    {
        var param = new List<string>(args);
        FileEntity file;
        var nfi = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        while (true)
        {
            if (param.Count != 0)
            {
                if (File.Exists(param[0]))
                {
                    file = new FileEntity(param[0]);
                    break;
                }

                Console.WriteLine($"Cant find path to file from CLI parameter: {param[0]}");
                param.Clear();
            }

            Console.Write("Enter path to file: ");
            var path = Console.ReadLine();

            if (File.Exists(path))
            {
                file = new FileEntity(path);
                break;
            }

            Console.WriteLine($"Cant find path to file: {path}");
        }

        var content = new ContentSeparator(file, nfi);

        Console.WriteLine(content.SeparatedSum.Max());

        foreach (var item in content.SeparatedBroken)
        {
            Console.Write($"{item} ");
        }
    }
}