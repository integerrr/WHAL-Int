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
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return DurationInSeconds.CompareTo(other.DurationInSeconds);
    }
}
