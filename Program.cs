using WHAL_Int.EggIncApi;
using WHAL_Int.Maj;

namespace WHAL_Int;
internal class Program
{
    public static async Task Main(string[] args)
    {
        if (string.IsNullOrEmpty(Config.EID))
        {
            Console.WriteLine("\"EID.txt\" not found in root directory, please create the file and only put your EID in the file.");
            return;
        }

        string testContractId = "starlink";
        var coopCodes = new List<string> { "padang283", "qamdo843", "minden586", "ovalle332", "gwadar600", "telde527" };

        var activeContract = await new ActiveContractBuilder(testContractId).Build();

        var tasks = new List<Task<Coop>>();
        foreach (string coopCode in coopCodes)
        {
            tasks.Add(activeContract.AddCoop(coopCode));
        }

        var coops = await Task.WhenAll(tasks);
        var orderedCoops = coops.OrderBy(x => x);
        foreach (var coop in orderedCoops)
        {
            Console.WriteLine($"{coop.CoopId}: {coop.PredictedDuration.DurationInSeconds} seconds total predicted.");
        }
    }
}
