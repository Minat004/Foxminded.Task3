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
}