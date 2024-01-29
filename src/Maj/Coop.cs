using Ei;
using WHAL_Int.Formatter;

namespace WHAL_Int.Maj;

public class Coop : IComparable<Coop>
{
    private readonly ContractCoopStatusResponse coopStatus;
    private readonly Contract.Types.GradeSpec gradeSpec;
    private double contractFarmMaximumTimeAllowed;
    private double coopAllowableTimeRemaining => coopStatus.SecondsRemaining;
    private double eggGoal => gradeSpec.Goals.MaxBy(g => g.TargetAmount)!.TargetAmount;
    private double shippedEggs => coopStatus.TotalAmount;

    private double totalShippingRate =>
        coopStatus.Contributors.Select(player => player.ContributionRate).Sum();

    // `FarmInfo.Timestamp` is basically (LastSyncUnix - currentUnix) in seconds, so the negative is required in the maths
    // Credits to WHALE for figuring out the maths for this :happywiggle:
    private double totalOfflineEggs =>
        coopStatus.Contributors.Select(player => player.ContributionRate * -(player.FarmInfo.Timestamp)).Sum();
    private double eggsRemaining => eggGoal - shippedEggs - totalOfflineEggs;
    private long predictedSecondsRemaining => Convert.ToInt64(eggsRemaining / totalShippingRate);
    private readonly long unixNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    public Coop(ContractCoopStatusResponse coopStatus, Contract contract)
    {
        this.coopStatus = coopStatus;
        gradeSpec = contract.GradeSpecs.SingleOrDefault(g => g.Grade == coopStatus.Grade)!;
        contractFarmMaximumTimeAllowed = contract.LengthSeconds;
        PredictedCompletionTimeUnix = new DiscordTimestamp(unixNow + predictedSecondsRemaining);
        PredictedDuration = new CoopDuration(Convert.ToInt64(contractFarmMaximumTimeAllowed -
                                                                  coopAllowableTimeRemaining +
                                                                  predictedSecondsRemaining));
    }

    /// <summary>
    /// Returns the Coop Code/ID of the Coop.
    /// </summary>
    public string CoopId => coopStatus.CoopIdentifier;
    /// <summary>
    /// Returns the Contract ID.
    /// </summary>
    public string ContractId => coopStatus.ContractIdentifier;
    /// <summary>
    /// Returns the first 6 characters of the Coop Code/ID. For use typically in formatted tables.
    /// </summary>
    public string StrippedCoopId => CoopId.Substring(0, 6);
    /// <summary>
    /// Returns the number of players that has spent more than or equal to 4 tokens.
    /// 4 tokens spent usually denotes that the particular player has boosted/began boosting in a SR setting.
    /// </summary>
    public int BoostedCount => coopStatus.Contributors.Count(x => x.BoostTokensSpent >= 4);
    /// <summary>
    /// Returns the combined total amount of tokens of all players in the Coop, including tokens that have been spent.
    /// </summary>
    public int TotalTokens => coopStatus.Contributors.Sum(x => (int)(x.BoostTokensSpent + x.BoostTokens));
    public DiscordTimestamp PredictedCompletionTimeUnix { get; private set; }
    public CoopDuration PredictedDuration { get; private set; }

    public int CompareTo(Coop? other)
    {
        if (other is null) return 1;
        int result = PredictedDuration.CompareTo(other.PredictedDuration);
        if (result == 0)
            result = other.BoostedCount.CompareTo(BoostedCount);
        if (result == 0)
            result = other.TotalTokens.CompareTo(TotalTokens);
        if (result == 0)
            result = PredictedCompletionTimeUnix.CompareTo(other.PredictedCompletionTimeUnix);
        return result;
    }
}
