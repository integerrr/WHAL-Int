using Ei;
using WHAL_Int.EggIncApi;
using WHAL_Int.Maj;
using WHAL_Int.src;

internal class Program
{
    public static async Task Main(string[] args)
    {
        if (string.IsNullOrEmpty(Config.EID))
        {
            Console.WriteLine("\"EID.txt\" not found in root directory, please create the file and only put your EID in the file.");
            return;
        }

        var coopCodes = new List<(string, string)>
        {
            ("spring-2019", "mortar845"),
            ("spring-2019", "bexley079"),
            ("spring-2019", "roller678"),
            ("spring-2019", "vannes204"),
            ("spring-2019", "jiexiu737"),
            ("spring-2019", "mosaic076"),
            ("spring-2019", "oshawa381")
        };

        var tasks = new List<Task<Coop>>();
        foreach (var (contractId, coopCode) in coopCodes)
        {
            var builder = new CoopBuilder(contractId, coopCode);
            tasks.Add(builder.Build());
        }

        var coops = await Task.WhenAll(tasks);
        foreach (var coop in coops)
        {
            Console.WriteLine($"{coop.CoopId}: {coop.TotalTokens:D2} tokens");
        }
    }
}
