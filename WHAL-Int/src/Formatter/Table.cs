using WHAL_Int.Maj;

namespace WHAL_Int.Formatter;

public class Table<T>
{
    private readonly Dictionary<string, Func<T, string>> columns = new();
    private readonly List<T> dataPoints = new();

    /*public Table(List<T> dataPoints) => this.dataPoints = dataPoints;*/

    public void AddColumn(string title, Func<T, string> colFunc) => columns.Add(title, colFunc);
    public void AddDataPoint(T dataPoint) => dataPoints.Add(dataPoint);

    public string GetTable()
    {
        string table = string.Join("|", columns.Keys.Select(c => c)) + "\n"
            + string.Join("\n", dataPoints.Select(x => string.Join("|", columns.Values.Select(f => f(x)))));

        return table;
    }
}
