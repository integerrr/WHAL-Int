using Ei;
using WHAL_Int.Formatter;

namespace WHAL_Int.Maj;

public class Coop : IComparable<Coop>
{
    private readonly ContractCoopStatusResponse coopStatus;
    private readonly Contract.Types.GradeSpec gradeSpec;
    private double contractFarmTimeLimit { get; init; }
    private double eggGoal => this.gradeSpec.Goals.MaxBy(g => g.TargetAmount)!.TargetAmount;
    private double shippedEggs => this.coopStatus.TotalAmount;

    private double totalShippingRate =>
        this.coopStatus.Contributors.Select(player => player.ContributionRate).Sum();

    // `FarmInfo.Timestamp` is basically (LastSyncUnix - currentUnix) in seconds, so the negative is required in the maths
    private double totalOfflineEggs =>
        this.coopStatus.Contributors.Select(player => player.ContributionRate * -(player.FarmInfo.Timestamp)).Sum();

    public Coop(ContractCoopStatusResponse coopStatus, Contract contract)
    {
        this.coopStatus = coopStatus;
        this.gradeSpec = contract.GradeSpecs.SingleOrDefault(g => g.Grade == coopStatus.Grade)!;
        this.contractFarmTimeLimit = contract.LengthSeconds;
        this.PredictedCompletionTimeUnix = new DiscordTimestamp(0);
        this.PredictedDuration = new CoopDuration(0);
    }

    public string CoopId => coopStatus.CoopIdentifier;
    public string ContractId => coopStatus.ContractIdentifier;
    public string StrippedCoopId => CoopId.Substring(0, 6);
    public int BoostedCount => coopStatus.Contributors.Count(x => x.BoostTokensSpent >= 6);
    public int TotalTokens => coopStatus.Contributors.Sum(x => (int)(x.BoostTokensSpent + x.BoostTokens));
    public double EggsRemaining => eggGoal - shippedEggs - totalOfflineEggs;
    public DiscordTimestamp PredictedCompletionTimeUnix { get; private set; }
    public CoopDuration PredictedDuration { get; private set; }

    public int CompareTo(Coop? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        int result = PredictedDuration.CompareTo(other.PredictedDuration);
        if (result == 0)
            result = BoostedCount.CompareTo(other.BoostedCount);
        if (result == 0)
            result = TotalTokens.CompareTo(other.TotalTokens);
        if (result == 0)
            result = PredictedCompletionTimeUnix.CompareTo(other.PredictedCompletionTimeUnix);
        return result;
    }
}
