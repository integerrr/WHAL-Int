using System.ComponentModel;

namespace WHAL_Int.Formatter;

public class StringFormatter
{
    public static string Left(string s, int width)
    {
        if (s.Length >= width) return s;

        int rightPadding = width - s.Length;

        return s + new string(' ', rightPadding);
    }

    public static string Centered(string s, int width)
    {
        if (s.Length >= width) return s;

        int leftPadding = (width - s.Length) / 2;
        int rightPadding = width - s.Length - leftPadding;

        return new string(' ', leftPadding) + s + new string(' ', rightPadding);
    }

    public static string Right(string s, int width)
    {
        if (s.Length >= width) return s;

        int leftPadding = width - s.Length;

        return new string(' ', leftPadding) + s;
    }

    public static string Align(string s, int width, StringAlignment alignment)
    {
        return alignment switch
        {
            StringAlignment.Left => Left(s, width),
            StringAlignment.Centered => Centered(s, width),
            StringAlignment.Right => Right(s, width),
            StringAlignment.None => s,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}

public enum StringAlignment
{
    Left,
    Centered,
    Right,
    None
}
