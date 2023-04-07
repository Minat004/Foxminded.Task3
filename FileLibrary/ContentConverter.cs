using System.Globalization;

namespace FileLibrary;

public class ContentConverter : IContentConverter
{
    public bool ToDecimal(string s, IFormatProvider format, out decimal value)
    {
        return decimal.TryParse(s, NumberStyles.Any, format, out value);
    }
}