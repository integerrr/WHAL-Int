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

        string contractId = "starlink";
        var coopCodes = new List<string> { "padang283", "qamdo843", "minden586", "ovalle332", "gwadar600", "telde527" };

        // these lines below would be done in a sub menu/file for sruc if we end up going multifunational and make a menu system
        var contract1 = await ContractProcessor.SetupCoops(contractId, coopCodes);
        Console.WriteLine(ContractProcessor.FormatContract(contract1, new SrucTableFormatter()));
    }
}
