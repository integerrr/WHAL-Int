namespace WHAL_Int.Formatter;

public class Duration : IComparable<Duration>
{
    public long DurationInSeconds { get; set; } = 0;

    public Duration(long durationInSeconds) => DurationInSeconds = durationInSeconds;

    public string Format()
    {
        throw new NotImplementedException();
    }

    public int CompareTo(Duration? other)
    {
        return other is null ? 1 : DurationInSeconds.CompareTo(other.DurationInSeconds);
    }
}
