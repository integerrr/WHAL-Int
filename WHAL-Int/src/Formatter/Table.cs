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

    public string GetTable()
    {
        string table = "`" +
                       string.Join("|", columns.Select(c => StringFormatter.Align(c.Name, c.Width, c.Alignment))) +
                       "`" +
                       "\n";

        foreach (T point in dataPoints)
        {
            table += string.Join("|", columns.Select(c => StringFormatter.Align(c.ColumnFunc(point), c.Width, c.Alignment))) +
                     "\n";
        }
        return table;
    }
}

public class TableColumn<T>(string name, Func<T, string> colFunc, int width, StringAlignment alignment)
{
    public string Name { get; } = name;
    public Func<T, string> ColumnFunc { get; } = colFunc;
    public int Width { get; } = width;
    public StringAlignment Alignment { get; } = alignment;
}
