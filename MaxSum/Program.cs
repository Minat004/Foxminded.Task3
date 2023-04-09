using System.Globalization;
using FileLibrary;

namespace MaxSum;

public static class Program
{
    public static void Main(string[] args)
    {
        FileEntity file;
        
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

        using (IContentStreamReader streamReader = new ContentStreamReader(file))
        {
            var content = new ContentSeparator(streamReader, nfi);

            Console.WriteLine();
            Console.WriteLine("List of SUM lines:");

            var sumArray = content.GetSum(out var brokenList);
            
            foreach (var item in sumArray)
            {
                Console.Write($"{item.ToString(nfi)} ");
            }
            
            Console.WriteLine("\n");

            Console.Write("MAX: ");
            Console.WriteLine(sumArray.Max().ToString(nfi));

            Console.WriteLine();
            Console.WriteLine("List of BROKEN indexes:");
            foreach (var item in brokenList)
            {
                Console.Write($"{item} ");
            }

            Console.ReadLine();
        }
    }
}