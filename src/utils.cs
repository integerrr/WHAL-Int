namespace WHAL_Int.Maj;

public class utils
{

    public const int SECONDS_IN_A_MINUTE = 60;
    public const int SECONDS_IN_AN_HOUR = SECONDS_IN_A_MINUTE * 60;
    public const int SECONDS_IN_A_DAY = SECONDS_IN_AN_HOUR * 24;

    /// <summary>
    /// Converts a Unix Epoch timestamp into a <c>DateTime</c> format
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns>
    /// <c>DateTime</c> object of the time passed in
    /// </returns>
    public static DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return origin.AddSeconds(timestamp);
    }

    /// <summary>
    /// Converts a <c>DateTime</c> format into a Unix Epoch timestamp
    /// </summary>
    /// <param name="date"></param>
    /// <returns>
    /// <c>Double</c> Unix Epoch timestamp
    /// </returns>
    public static double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return Math.Floor(diff.TotalSeconds);
    }
}
