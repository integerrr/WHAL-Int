using WHAL_Int.Formatter;
using Xunit;

namespace UnitTestProject.Formatter;

public class DurationTests
{
    /// <summary>
    /// Test that constructor sets the given duration
    /// </summary>
    [Fact]
    public void TestConstructor()
    {
        var duration = new Duration(5L);
        Assert.Equal(5L, duration.DurationInSeconds);
    }

    [Theory]
    [InlineData(0, "0m")]
    [InlineData(1, "0m")]
    [InlineData(59, "0m")]
    [InlineData(60, "1m")]
    [InlineData(3599, "59m")]
    [InlineData(3600, "1h0m")]
    [InlineData(3660, "1h1m")]
    [InlineData(86399, "23h59m")]
    [InlineData(86400, "1d0m")]
    [InlineData(89999, "1d59m")]
    [InlineData(90000, "1d1h0m")]
    public void TestFormat(long internalDuration, string expectedOutput)
    {
        var duration = new Duration(internalDuration);
        Assert.Equal(expectedOutput, duration.Format());
    }
}
