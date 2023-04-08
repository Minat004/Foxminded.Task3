using Autofac.Extras.Moq;

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

            var cls = mock.Create<ContentSeparator>();

            var expected = GetLines();

            var actual = cls.GetAllLines();
            
            Assert.True(actual != null);
            Assert.Equal(expected, actual!);
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
}