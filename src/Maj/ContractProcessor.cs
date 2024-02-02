using WHAL_Int.Formatter;

namespace WHAL_Int.Maj;

public class ContractProcessor
{
    public static async Task<ActiveContract> SetupCoops(string contractId, List<string> coopCodes)
    {
        var activeContract = await new ActiveContractBuilder(contractId).Build();
        foreach (string coopCode in coopCodes)
        {
            await activeContract.AddCoop(coopCode);
        }
        return activeContract;
    }

    public static string FormatContract(ActiveContract activeContract, IContractFormatter formatter)
    {
        return formatter.Format(activeContract);
    }
}
