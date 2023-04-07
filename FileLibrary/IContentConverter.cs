namespace FileLibrary;

public interface IContentConverter
{
    public bool ToDecimal(string s, IFormatProvider format, out decimal value);
}