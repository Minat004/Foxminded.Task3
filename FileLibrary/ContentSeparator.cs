using System.Globalization;

namespace FileLibrary;

public class ContentSeparator
{
    private readonly IFormatProvider _format;
    private readonly IContentStreamReader _streamReader;

    public ContentSeparator(
        IContentStreamReader streamReader,
        IFormatProvider format
    )
    {
        _format = format;
        _streamReader = streamReader;
    }

    public List<List<string>> GetSeparatedText(char separator = ',')
    {
        var result = new List<List<string>>();
        
        foreach (var line in _streamReader.ReadLines())
        {
            result.Add(new List<string>(line!.Trim().Split(separator).ToArray()));
        }

        return result;
    }

    public List<decimal> GetSum(out List<int> brokenList)
    {
        brokenList = new List<int>();
        var sumList = new List<decimal>();
        var separatedText = GetSeparatedText();

        for (var i = 0; i < separatedText.Count; i++)
        {
            var sum = 0m;
            var isBroken = false;
            
            foreach (var number in separatedText[i])
            {
                if (decimal.TryParse(number, NumberStyles.Any, _format, out var res))
                {
                    sum += res;
                }
                else
                {
                    brokenList.Add(i + 1);
                    isBroken = true;
                    break;
                }
            }

            if (!isBroken)
            {
                sumList.Add(sum);
            }
        }

        return sumList;
    }
}