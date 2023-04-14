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
        var index = 1;

        foreach (var line in _streamReader.ReadLines())
        {
            var sum = decimal.Zero;
            var isBroken = false;
            var separated = line!.Trim().Split(separator);
            var isEmpty = true;

            for (var i = 0; i < separated.Length; i++)
            {
                if (decimal.TryParse(separated[i], NumberStyles.Any, CultureInfo.InvariantCulture, out var res))
                {
                    sum += res;
                    isEmpty = false;
                }
                else if (!string.IsNullOrEmpty(separated[i].Trim()) || (isEmpty && i == separated.Length - 1))
                {
                    brokenList.Add(index);
                    isBroken = true;
                    break;
                }
            }

            if (!isBroken)
            {
                sumList.Add(index, sum);
            }

            index++;
        }

        return sumList;
    }
}