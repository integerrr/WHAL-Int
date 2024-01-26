using WHAL_Int.EggIncApi;
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

        string testContractId = "spring-2019";
        var coopCodes = new List<string>{"mortar845", "bexley079", "roller678", "vannes204", "jiexiu737", "mosaic076", "oshawa381"};

        var activeContract = await new ActiveContractBuilder(testContractId).Build();

        var tasks = new List<Task<Coop>>();
        foreach (string coopCode in coopCodes)
        {
            tasks.Add(activeContract.AddCoop(coopCode));
        }

        var coops = await Task.WhenAll(tasks);
        foreach (var coop in coops)
        {
            Console.WriteLine($"{coop.CoopId}: {coop.TotalTokens:D2} tokens");
        }
    }
}
