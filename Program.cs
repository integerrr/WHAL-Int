using WHAL_Int.EggIncApi;
using WHAL_Int.Formatter;
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

        string contractId = "passive-challenge-2024";
        var coopCodes = new List<string> { "locate642", "indeed825", "almost150", "avenue845", "injury372" };

        var activeContract = await new ActiveContractBuilder(contractId).Build();
        foreach (string coopCode in coopCodes)
        {
            await activeContract.AddCoop(coopCode);
        }

        Console.WriteLine(new SrucTableFormatter().Format(activeContract));
    }
}
