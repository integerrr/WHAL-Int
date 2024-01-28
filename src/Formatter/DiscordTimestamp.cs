namespace WHAL_Int.Formatter;

public class DiscordTimestamp
{
    public int UnixSeconds { get; set; } = 0;

    public DiscordTimestamp(int secondsSinceEpoch) => UnixSeconds = secondsSinceEpoch;

    public string Format(DiscordTimestampDisplay displayMode)
    {
        char identifier = displayMode switch
        {
            DiscordTimestampDisplay.ShortDate => 'd',
            DiscordTimestampDisplay.FullDate => 'D',
            DiscordTimestampDisplay.HourMinuteTime => 't',
            DiscordTimestampDisplay.HourMinuteSecondTime => 'T',
            DiscordTimestampDisplay.FullDateTime => 'f',
            DiscordTimestampDisplay.FullDateTimeDayOfWeek => 'F',
            DiscordTimestampDisplay.Relative => 'R',
            _ => 'f'
        };

        return $"`<t:{UnixSeconds}:{identifier}>`";
    }
}

public enum DiscordTimestampDisplay
{
    ShortDate,
    FullDate,
    HourMinuteTime,
    HourMinuteSecondTime,
    FullDateTime,
    FullDateTimeDayOfWeek,
    Relative,
}
