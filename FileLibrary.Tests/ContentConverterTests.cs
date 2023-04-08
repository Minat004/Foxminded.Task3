using System.Globalization;

namespace FileLibrary.Tests;

public class ContentConverterTests
{
    [Theory]
    [InlineData("15.23", 15.23, true)]
    [InlineData("1", 1, true)]
    [InlineData("A", 0, false)]
    [InlineData("0", 0, true)]
    [InlineData("0.000001.00001", 0, false)]
    public void ToDecimalTest(string inputString, decimal expected, bool expectedTryParse)
    {
        var converter = new ContentConverter();
        
        var nfi = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        var actualParse = converter.ToDecimal(inputString, nfi, out var actual);
        
        Assert.Equal(expectedTryParse, actualParse);
        Assert.Equal(expected, actual);
    }
    
}