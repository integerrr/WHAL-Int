namespace WHAL_Int.Formatter;

public class CoopDuration : IComparable<CoopDuration>
{
    public long DurationInSeconds { get; set; } = 0;

    public CoopDuration(long durationInSeconds) => DurationInSeconds = durationInSeconds;

    public string Format()
    {
        throw new NotImplementedException();
    }

    public int CompareTo(CoopDuration? other)
    {
        return other is null ? 1 : DurationInSeconds.CompareTo(other.DurationInSeconds);
    }
}
