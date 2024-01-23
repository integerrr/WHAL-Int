using Ei;
using WHAL_Int.EggIncApi;

namespace WHAL_Int.Maj;

public class Coop
{
    private string contractId = "";
    private string coopId = "";
    private ContractCoopStatusResponse? coopStatus;

    public Coop(string contractId, string coopId)
    {
        this.contractId = contractId;
        this.coopId = coopId;

        requestCoopStatus();
    }

    public string CoopId() => coopId;
    public string ContractId() => contractId;
    public string StrippedCoopId() => coopId.Substring(0, 6);
    public uint BoostedCount()
    {
        uint count = 0;
        foreach (var player in coopStatus.Contributors)
        {
            if (player.BoostTokensSpent >= 6)
            {
                count++;
            }
        }
        return count;
    }

    public uint TotalTokens()
    {
        uint count = 0;
        foreach (var player in coopStatus.Contributors)
        {
            count += player.BoostTokensSpent;
            count += player.BoostTokens;
        }
        return count;
    }

    private async void requestCoopStatus()
    {
        coopStatus = await Request.GetCoopStatus(contractId, coopId);
    }
}
