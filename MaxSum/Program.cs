using System.Globalization;
using FileLibrary;

namespace MaxSum;

public static class Program
{
    public static void Main(string[] args)
    {
        string path;
        
        var param = new List<string>(args);
        
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
                    path = param[0];
                    break;
                }

                Console.WriteLine($"Cant find path to file from CLI parameter: {param[0]}");
                param.Clear();
            }

            Console.Write("Enter path to file: ");
            var pathIn = Console.ReadLine();

            if (File.Exists(pathIn))
            {
                path = pathIn;
                break;
            }

            Console.WriteLine($"Cant find path to file: {pathIn}");
        }

        IContentStreamReader streamReader = new ContentStreamReader(path);
        var content = new ContentSeparator(streamReader, nfi);

        Console.WriteLine();
        Console.WriteLine("List of SUM lines:");

        var sumArray = content.GetSum(out var brokenList);

        Console.WriteLine();
        Console.Write("Index of MAX: ");
        Console.WriteLine(sumArray.IndexOf(sumArray.Max() + 1));

        Console.WriteLine();
        Console.WriteLine("List of BROKEN indexes:");
        foreach (var item in brokenList)
        {
            Console.Write($"{item} ");
        }

        Console.ReadLine();
    }
}