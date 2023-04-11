using System.Globalization;

namespace FileLibrary;

public class ContentSeparator
{
    private readonly IContentStreamReader _streamReader;

    public ContentSeparator(IContentStreamReader streamReader)
    {
        _streamReader = streamReader;
    }

    public Dictionary<int, decimal> GetDictOfSum(out List<int> brokenList, char separator = ',')
    {
        brokenList = new List<int>();
        var sumList = new Dictionary<int, decimal>();
        var lines = new List<string>((IEnumerable<string>)_streamReader.ReadLines());

        for (var i = 0; i < lines.Count; i++)
        {
            var sum = 0m;
            var isBroken = false;
            var separated = lines[i].Trim().Split(separator);
            
            foreach (var number in separated)
            {
                if (decimal.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out var res))
                {
                    sum += res;
                }
                else if (!string.IsNullOrEmpty(number.Trim()) || separated[^1] == string.Empty)
                {
                    brokenList.Add(i + 1);
                    isBroken = true;
                    break;
                }
            }

            if (!isBroken)
            {
                sumList.Add(i + 1, sum);
            }
        }

        return sumList;
    }
}