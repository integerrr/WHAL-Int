using Ei;

namespace WHAL_Int.Maj;

public class Coop : IComparable<Coop>
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
    public int PredictedCompletionTimeUnix;
    public int CompareTo(Coop? other)
    {

    }
}
