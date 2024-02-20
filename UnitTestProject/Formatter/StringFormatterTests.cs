using System.ComponentModel;
using WHAL_Int.Formatter;
using Xunit;

namespace UnitTestProject.Formatter;

public class StringFormatterTests
{
    [Theory]
    [InlineData("whale", 0, "whale")]
    [InlineData("whale", 5, "whale")]
    [InlineData("whale", 10, "whale     ")]
    [InlineData("WHALOG", 10, "WHALOG    ")]
    public void TestLeft(string inputStr, int width, string expectedStr)
    {
        string actual = StringFormatter.Left(inputStr, width);
        Assert.Equal(expectedStr, actual);
    }

    [Theory]
    [InlineData("whale", 0, "whale")]
    [InlineData("whale", 5, "whale")]
    [InlineData("whale", 10, "  whale   ")]
    [InlineData("WHALOG", 10, "  WHALOG  ")]
    public void TestCentered(string inputStr, int width, string expectedStr)
    {
        string actual = StringFormatter.Centered(inputStr, width);
        Assert.Equal(expectedStr, actual);
    }

    [Theory]
    [InlineData("whale", 0, "whale")]
    [InlineData("whale", 5, "whale")]
    [InlineData("whale", 10, "     whale")]
    [InlineData("WHALOG", 10, "    WHALOG")]
    public void TestRight(string inputStr, int width, string expectedStr)
    {
        string actual = StringFormatter.Right(inputStr, width);
        Assert.Equal(expectedStr, actual);
    }

    [Theory]
    [InlineData("WHAlog", 10, StringAlignment.Left, "WHAlog    ")]
    [InlineData("WHAlog", 10, StringAlignment.Centered, "  WHAlog  ")]
    [InlineData("WHAlog", 10, StringAlignment.Right, "    WHAlog")]
    [InlineData("WHAlog", 10, StringAlignment.None, "WHAlog")]
    public void TestAlign(string inputStr, int width, StringAlignment alignment, string expectedStr)
    {
        string actual = StringFormatter.Align(inputStr, width, alignment);
        Assert.Equal(expectedStr, actual);
    }

    [Fact]
    public void TestAlignException()
    {
        Action action = () => StringFormatter.Align("", 0, StringAlignment.None + 1);
        InvalidEnumArgumentException e = Assert.Throws<InvalidEnumArgumentException>(action);
        Assert.IsType<InvalidEnumArgumentException>(e);
    }
}
