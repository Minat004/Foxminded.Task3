using FileLibrary;

namespace MaxSum;

public static class App
{
    public static string InputValidator(IEnumerable<string> args)
    {
        var param = new List<string>(args);

        while (true)
        {
            if (param.Count != 0)
            {
                if (File.Exists(param[0]))
                {
                    return param[0];
                }

                Console.WriteLine($"Cant find path to file from CLI parameter: {param[0]}");
                param.Clear();
            }

            Console.Write("Enter path to file: ");
            var pathIn = Console.ReadLine();

            if (File.Exists(pathIn))
            {
                return pathIn;
            }

            Console.WriteLine($"Cant find path to file: {pathIn}");
        }
    }

    public static void Output(string value)
    {
        try
        {
            IContentStreamReader streamReader = new ContentStreamReader(value);
            var content = new ContentSeparator(streamReader);

            var sumArray = content.GetDictOfSum(out var brokenList);
            var index = 
                sumArray
                    .FirstOrDefault(x => x.Value == sumArray.Max(k => k.Value)).Key;

            Console.WriteLine();
            Console.Write("Index of MAX: ");
            Console.WriteLine(index);

            Console.WriteLine();
            Console.Write("List of BROKEN indexes: ");
        
            foreach (var item in brokenList)
            {
                Console.Write($"{item} ");
            }

            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cant read the file {value}!");
            Console.WriteLine(e);
        }
    }
}