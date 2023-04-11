using Autofac.Extras.Moq;

namespace FileLibrary.Tests;

public class ContentSeparatorTests
{
    [Fact]
    public void GetDictOfSumTest()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IContentStreamReader>()
                .Setup(x => x.ReadLines())
                .Returns(GetLines());

            var cls = mock.Create<ContentSeparator>();
            
            var expectedSum = GetDictOfSums();
            var expectedBroken = GetBrokenIndexes();
        
            var actualSum = cls.GetDictOfSum(out var actualBroken);
            
            Assert.True(actualSum !=null);
            Assert.True(actualBroken !=null);
            Assert.Equal(expectedSum, actualSum);
            Assert.Equal(expectedBroken, actualBroken);
        }
    }
    
    [Fact]
    public void GetDictOfSumHardTest()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IContentStreamReader>()
                .Setup(x => x.ReadLines())
                .Returns(GetHardLines());

            var cls = mock.Create<ContentSeparator>();
            
            var expectedSum = GetHardDictOfSums();
            var expectedBroken = GetHardBrokenIndexes();
        
            var actualSum = cls.GetDictOfSum(out var actualBroken);
            
            Assert.True(actualSum !=null);
            Assert.True(actualBroken !=null);
            Assert.Equal(expectedSum, actualSum);
            Assert.Equal(expectedBroken, actualBroken);
        }
    }

    private static IEnumerable<string?> GetLines()
    {
        var result = new List<string>
        {
            "1,2,3,4,5,6,7,8,9,0",
            "1,2,3,4,5,j6,7,8,9,0",
            "2.0,3.1,4.4,5.9,100",
            "12.12134765974893,5674234.01",
            "1,2,3,4,5,j6,7,8,9,0a"
        };
            
        return result;
    }
    
    private static IEnumerable<string?> GetHardLines()
    {
        var result = new List<string>
        {
            "2.0,3.1,4.4,5.9,100",
            "12.12134765974893,5674234.01",
            "100, 486a4sad4, 45,",
            "100.0,99.9,98,97,96,1000.123",
            "1,2,3,4,5,j6,7,8,9,0a",
            "1,2,  3, 4,5,j    6,7,8,9,0a",
            "1,2,3,4,5,j6,7,8,9,0a",
            "1,2,3,  4,5  ,j6,7,8,9,0a",
            "1,2,3,4,5,j6,7,8,9,0a",
            "1,2,3,4,5,j6,7,8,9,0a",
            "1,2,3,4,5,j6,7,8,9,0a",
            "1.234,1.789,",
            "1, 2 , 3 , 4 , 5 , j 6 ,  7 , 8 , 9 , 0 a "
        };
            
        return result;
    }

    private static Dictionary<int, decimal> GetHardDictOfSums()
    {
        var result = new Dictionary<int, decimal>
        {
            { 1, 115.4m },
            { 2, 5674246.13134765974893m },
            { 4, 1491.023m },
            { 12, 3.023m }
        };

        return result;
    }
    
    private static Dictionary<int, decimal> GetDictOfSums()
    {
        var result = new Dictionary<int, decimal>
        {
            { 1, 45m },
            { 3, 115.4m },
            { 4, 5674246.13134765974893m }
        };

        return result;
    }

    private static List<int> GetBrokenIndexes()
    {
        return new List<int> { 2, 5 };
    }
    
    private static List<int> GetHardBrokenIndexes()
    {
        return new List<int> { 3, 5, 6, 7, 8, 9, 10, 11, 13 };
    }
}