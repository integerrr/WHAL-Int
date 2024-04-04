using WHAL_Int.Formatter;
using Xunit;

namespace UnitTestProject.Formatter;

public class DiscordTimestampTests
{
    /// <summary>
    /// Test that the constructor sets the given unix timestamp
    /// </summary>
    [Fact]
    public void TestConstructor()
    {
        var timestamp = new DiscordTimestamp(5L);
        Assert.Equal(5L, timestamp.UnixSeconds);
    }

    /// <summary>
    /// Test that the <see cref="DiscordTimestamp.Format"/> method returns the correctly formatted string for all
    /// possible formats
    /// </summary>
    /// <param name="format">Format to test</param>
    /// <param name="expectedResult">The expected result when formatting with <paramref name="format"/></param>
    [Theory]
    [InlineData(DiscordTimestampDisplay.ShortDate, "<t:123:d>")]
    [InlineData(DiscordTimestampDisplay.FullDate, "<t:123:D>")]
    [InlineData(DiscordTimestampDisplay.HourMinuteTime, "<t:123:t>")]
    [InlineData(DiscordTimestampDisplay.HourMinuteSecondTime, "<t:123:T>")]
    [InlineData(DiscordTimestampDisplay.FullDateTime, "<t:123:f>")]
    [InlineData(DiscordTimestampDisplay.FullDateTimeDayOfWeek, "<t:123:F>")]
    [InlineData(DiscordTimestampDisplay.Relative, "<t:123:R>")]
    public void TestFormat(DiscordTimestampDisplay format, string expectedResult)
    {
        var timestamp = new DiscordTimestamp(123L);
        Assert.Equal(expectedResult, timestamp.Format(format));
    }


    /// <summary>
    /// Test that <see cref="DiscordTimestamp.CompareTo(DiscordTimestamp?)"/> correctly returns an ordering based on the unix timestamp
    /// </summary>
    /// <param name="this">Unix timestamp of the "this" timestamp</param>
    /// <param name="other">Unis timestamp of the "other" timestamp, or null</param>
    /// <param name="expected">Expected result. Only matters whether it's negative, zero, positive</param>
    [Theory]
    [InlineData(10L, 20L, -1)]
    [InlineData(15L, 15L, 0)]
    [InlineData(20L, 10L, 1)]
    [InlineData(10L, null, 1)]
    public void TestCompareTo(long @this, long? other, int expected)
    {
        var thisTimestamp = new DiscordTimestamp(@this);
        var otherTimestamp = other.HasValue ? new DiscordTimestamp(other.Value) : null;
        var compareResult = thisTimestamp.CompareTo(otherTimestamp);
        // Even though CompareTo usually returns -1, 0, 1, it is technically only necessary to return a negative, zero, postive result
        // Therefore, we use "expected" to check which is expected, but don't do a direct comparison
        if (expected == 0)
        {
            Assert.Equal(0, compareResult);
        }
        else if (expected < 0)
        {
            Assert.True(compareResult < 0);
        }
        else
        {
            Assert.True(compareResult > 0);
        }
    }
}
