using Ei;

namespace WHAL_Int.Maj;

public class Coop : IComparable<Coop>
{
    private readonly ContractCoopStatusResponse coopStatus;
    private readonly Contract.Types.GradeSpec gradeSpec;

    public Coop(ContractCoopStatusResponse coopStatus, Contract contract)
    {
        this.coopStatus = coopStatus;
        this.gradeSpec = contract.GradeSpecs.SingleOrDefault(g => g.Grade == coopStatus.Grade)!;
        this.ContractFarmTimeLimit = contract.LengthSeconds;
    }

    public string CoopId => coopStatus.CoopIdentifier;
    public string ContractId => coopStatus.ContractIdentifier;
    public string StrippedCoopId => CoopId.Substring(0, 6);
    public int BoostedCount => coopStatus.Contributors.Count(x => x.BoostTokensSpent >= 6);
    public int TotalTokens => coopStatus.Contributors.Sum(x => (int)(x.BoostTokensSpent + x.BoostTokens));
    public int PredictedCompletionTimeUnix { get; } = 0;
    public double ContractFarmTimeLimit { get; init; }
    public double TargetShippedEggs => this.gradeSpec.Goals.MaxBy(g => g.TargetAmount)!.TargetAmount;
    public int CompareTo(Coop? other)
    {

    }
}
