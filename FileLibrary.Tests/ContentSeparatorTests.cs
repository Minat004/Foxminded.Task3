using System.Globalization;
using Autofac.Extras.Moq;
using Moq;

namespace FileLibrary.Tests;

public class ContentSeparatorTests
{
    [Fact]
    public void GetAllLinesTest()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IContentStreamReader>()
                .Setup(x => x.ReadLines())
                .Returns(GetLines());

            var mockSeparator = mock.Create<ContentSeparator>();

            var expected = GetLines();

            var actual = mockSeparator.GetAllLines();
            
            Assert.True(actual != null);
            Assert.Equal(expected, actual);
        }
    }

    [Fact]
    public void GetSeparatedTextTest()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IContentStreamReader>()
                .Setup(x => x.ReadLines())
                .Returns(GetLines());
            
            var mockSeparator = mock.Create<ContentSeparator>();
            
            var expected = GetTexts();
        
            var actual = mockSeparator.GetSeparatedText();
        
            Assert.True(actual !=null);
            Assert.Equal(expected, actual);
        }
    }

    [Fact]
    public void GetSumTest()
    {
        var mock = new Mock<IContentStreamReader>();
        var format = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };
        
        mock.Setup(x => x.ReadLines()).Returns(GetLines());
        
        var separator = new ContentSeparator(mock.Object, format);
        
        var expectedSum = GetSums();
        var expectedBroken = GetBrokenIndexes();
        
        var actualSum = separator.GetSum(out var actualBroken);
        
        Assert.True(actualSum !=null);
        Assert.True(actualBroken !=null);
        Assert.Equal(expectedSum, actualSum);
        Assert.Equal(expectedBroken, actualBroken);
    }

    private static List<string?> GetLines()
    {
        var result = new List<string>
        {
            "1,2,3,4,5,6,7,8,9,0",
            "1,2,3,4,5,j6,7,8,9,0",
            "2.0,3.1,4.4,5.9,100",
            "12.12134765974893,5674234.01",
            "1,2,3,4,5,j6,7,8,9,0a"
        };
            
        return result!;
    }
    
    private static List<List<string>> GetTexts()
    {
        var result = new List<List<string>>
        {
            new()
            {
                "1","2","3","4","5","6","7","8","9","0"
            },
            new()
            {
                "1","2","3","4","5","j6","7","8","9","0"
            },
            new()
            {
                "2.0","3.1","4.4","5.9","100"
            },
            new()
            {
                "12.12134765974893","5674234.01"
            },
            new()
            {
                "1","2","3","4","5","j6","7","8","9","0a"
            }
        };
            
        return result;
    }

    private static List<decimal> GetSums()
    {
        var result = new List<decimal>
        {
            45m,
            115.4m,
            5674246.13134765974893m
        };

        return result;
    }

    private static List<int> GetBrokenIndexes()
    {
        return new List<int>() { 1, 4 };
    }
}