using System.Drawing;
using WHAL_Int.Maj;

namespace WHAL_Int.Formatter;

public class Table<T>
{
    private readonly Dictionary<string, (Func<T, string>, int, StringAlignment)> columns = new();
    private readonly List<T> dataPoints = new();

    public void AddColumn(
        string title,
        Func<T, string> colFunc,
        int colWidth = 5,
        StringAlignment alignment = StringAlignment.Centered
    ) => columns.Add(title, (colFunc, colWidth, alignment));

    public void AddDataPoint(T dataPoint) => dataPoints.Add(dataPoint);

    public string GetTable()
    {
        /*
        string table = string.Join("|", columns.Keys.Select(c => c)) + "\n"
            + string.Join("\n", dataPoints.Select(x => string.Join("|", columns.Values.Select(f => f(x)))));
        */

        string table = string.Join("|", columns.Keys.Select(c => c)) + "\n";
        foreach (T point in dataPoints)
        {
            table += string.Join("|", columns.Values.Select(f => StringFormatter.Align(f.Item1(point), f.Item2, f.Item3))) + "\n";
        }

        return table;
    }
}
