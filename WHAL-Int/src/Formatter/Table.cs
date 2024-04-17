namespace WHAL_Int.Formatter;

public class Table<T>
{
    private readonly List<TableColumn<T>> columns = [];
    private readonly List<T> dataPoints = [];

    public void AddColumn(
        string title,
        Func<T, string> colFunc,
        int colWidth = 5,
        StringAlignment alignment = StringAlignment.Centered
    ) => columns.Add(new TableColumn<T>(title, colFunc, colWidth, alignment));

    public void AddDataPoint(T dataPoint) => dataPoints.Add(dataPoint);

    public string GetTableHeader() =>
        "`" +
        string.Join("|", columns.Select(c => StringFormatter.Align(c.Name, c.Width, c.Alignment))) +
        "`";

    public string GetTableBody()
    {
        string body = "";
        foreach (T point in dataPoints)
        {
            body += string.Join("|",
                columns.Select(c => StringFormatter.Align(c.ColumnFunc(point), c.Width, c.Alignment)));
            body += point!.Equals(dataPoints.Last()) ? "" : "\n";
        }
        return body;
    }
}

public class TableColumn<T>(string name, Func<T, string> colFunc, int width, StringAlignment alignment)
{
    public string Name { get; } = name;
    public Func<T, string> ColumnFunc { get; } = colFunc;
    public int Width { get; } = width;
    public StringAlignment Alignment { get; } = alignment;
}
