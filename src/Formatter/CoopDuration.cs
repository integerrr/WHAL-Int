namespace WHAL_Int.Formatter;

public class CoopDuration : IComparable<CoopDuration>
{
    public const int SECONDS_IN_A_MINUTE = 60;
    public const int SECONDS_IN_AN_HOUR = SECONDS_IN_A_MINUTE * 60;
    public const int SECONDS_IN_A_DAY = SECONDS_IN_AN_HOUR * 24;

    public long DurationInSeconds { get; set; } = 0;

    public CoopDuration(long durationInSeconds) => DurationInSeconds = durationInSeconds;

    public string Format()
    {
        long day = DurationInSeconds / SECONDS_IN_A_DAY;
        long hour = (DurationInSeconds - day * SECONDS_IN_A_DAY) / SECONDS_IN_AN_HOUR;
        long min = (DurationInSeconds - day * SECONDS_IN_A_DAY - hour * SECONDS_IN_AN_HOUR) / SECONDS_IN_A_MINUTE;

        if (day > 99) return "too long";

        string str = day > 0 ? $"{day}d" : "";
        str += hour > 0 ? $"{hour}h" : "";
        str += $"{min}m";
        return str;
    }

    public int CompareTo(CoopDuration? other)
    {
        return other is null ? 1 : DurationInSeconds.CompareTo(other.DurationInSeconds);
    }
}
