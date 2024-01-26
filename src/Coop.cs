using Ei;
using WHAL_Int.EggIncApi;

namespace WHAL_Int.src;

public class Coop
{
    private ContractCoopStatusResponse coopStatus;

    public Coop(ContractCoopStatusResponse coopStatus)
    {
        this.coopStatus = coopStatus;
    }

    public string CoopId => coopStatus.CoopIdentifier;
    public string ContractId => coopStatus.ContractIdentifier;
    public string StrippedCoopId => CoopId.Substring(0, 6);
    public int BoostedCount => coopStatus.Contributors.Count(x => x.BoostTokensSpent >= 6);
    public int TotalTokens => coopStatus.Contributors.Sum(x => (int)(x.BoostTokensSpent + x.BoostTokens));
}
