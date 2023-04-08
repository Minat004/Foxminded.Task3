using System.Collections.ObjectModel;

namespace FileLibrary;

public class ContentSeparator
{
    private readonly IFormatProvider _format;

    public ContentSeparator(
        IContentStreamReader streamReader,
        IFormatProvider format
    )
    {
        _format = format;
        _streamReader = streamReader;

        SeparatedSum = new ReadOnlyCollection<decimal>(GetSum(out var brokenList));
        SeparatedBroken = new ReadOnlyCollection<int>(brokenList);
    }

    private readonly IContentStreamReader _streamReader;

    public ReadOnlyCollection<decimal> SeparatedSum { get; }
    
    public ReadOnlyCollection<int> SeparatedBroken { get; }

    public List<string?> GetAllLines()
    {
        return _streamReader.ReadLines();
    }

    private List<List<string>> GetSeparatedText(char separator = ',')
    {
        var result = new List<List<string>>();
        
        foreach (var line in GetAllLines())
        {
            result.Add(new List<string>(line!.Trim().Split(separator).ToArray()));
        }

        return result;
    }

    private List<decimal> GetSum(out List<int> brokenList)
    {
        brokenList = new List<int>();
        var sumList = new List<decimal>();
        var converter = new ContentConverter();
        var separatedText = GetSeparatedText();

        for (var i = 0; i < separatedText.Count; i++)
        {
            var sum = 0m;
            
            foreach (var number in separatedText[i])
            {
                if (converter.ToDecimal(number, _format, out var res))
                {
                    sum += res;
                }
                else
                {
                    brokenList.Add(i);
                    break;
                }
            }

            sumList.Add(sum);
        }

        return sumList;
    }
}