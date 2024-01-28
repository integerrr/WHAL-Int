namespace WHAL_Int.Formatter;

public class CoopDuration
{
    public int DurationInSeconds { get; set; } = 0;

    public CoopDuration(int durationInSeconds) => DurationInSeconds = durationInSeconds;

    public string Format()
    {
        throw new NotImplementedException();
    }
}
