using Ei;
using WHAL_Int.EggIncApi;

namespace WHAL_Int.Maj;

public class Coop
{
    private ContractCoopStatusResponse coopStatus;

    public Coop(string contractId, string coopId)
    {
        coopStatus = Request.GetCoopStatus(contractId, coopId).Result;
    }

    public string CoopId() => coopStatus.CoopIdentifier;
    public string ContractId() => coopStatus.ContractIdentifier;
    public string StrippedCoopId() => CoopId().Substring(0, 6);
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

    /// <summary>
    /// Get the completion time of the coop in Unix Epoch time format. This accounts for players offline time.
    /// </summary>
    /// <returns>
    /// <c>double</c> Unix Epoch timestamp of the coops completion time.
    /// </returns>
    public double UnixCompletionTime()
    {
        double unixTimestamp = utils.ConvertToUnixTimestamp(DateTime.UtcNow);

        // Get completion time if all goals have been met and the coop is done
        if (coopStatus.HasSecondsSinceAllGoalsAchieved)
        {
            return Math.Floor(unixTimestamp - coopStatus.SecondsSinceAllGoalsAchieved);
        }

        // Find how many eggs are left to produce until the final goal is reached
        double amountLeft = Request.GetContract(ContractId())   // Get the target eggs for final AAA reward
            .GradeSpecs[4]                                      // Will correct this in future to accept any grade
            .Goals
            .Last()
            .TargetAmount
            - coopStatus.TotalAmount;   // Coops current contributed eggs
        double totalProductionRateInSecs = 0;   // All players contribution rates

        // Calculate each players contribution during offline time
        foreach (var player in coopStatus.Contributors)
        {
            double offlineTimeInSeconds = -player.FarmInfo.Timestamp;
            double productionRateInSecs = player.ContributionRate;

            amountLeft -= offlineTimeInSeconds * productionRateInSecs;
            totalProductionRateInSecs += productionRateInSecs;  // Add players contribution to the total
        }

        // Calculate time to complete remaining eggs to ship
        double timeToCompleteInSeconds = amountLeft / totalProductionRateInSecs;

        return Math.Min(Math.Floor(unixTimestamp + timeToCompleteInSeconds),9999999999);
    }

    /// <summary>
    /// Gets the total duration of the coop in its current state, from start to projected completion
    /// </summary>
    /// <returns>
    /// <c>string</c> in the format of <c>dd/hh/mm</c>, and "too long" if it's more than 99 days
    /// </returns>
    public string Duration()
    {
        // Calculate Unix timestamp of the completion deadline,
        double completionDeadlineEpoch = utils.ConvertToUnixTimestamp(DateTime.UtcNow.AddSeconds(coopStatus.SecondsRemaining));
        // find how many seconds completion time is from deadline,
        double extraTimeInCoopInSecs = completionDeadlineEpoch - UnixCompletionTime();
        // and finally subtract that from the contract length
        double coopDurationInSecs = Request.GetContract(ContractId()).GradeSpecs[4].LengthSeconds - extraTimeInCoopInSecs;

        // Find the total number of days, hours and minutes from the duration
        double day = Math.Floor(coopDurationInSecs / utils.SECONDS_IN_A_DAY);
        double hour = Math.Floor((coopDurationInSecs - day * utils.SECONDS_IN_A_DAY) / utils.SECONDS_IN_AN_HOUR);
        double min = Math.Floor((coopDurationInSecs - day * utils.SECONDS_IN_A_DAY - hour * utils.SECONDS_IN_AN_HOUR) / utils.SECONDS_IN_A_MINUTE);

        // Convert into string format of dd/hh/mm
        string coopDurationAsString = "";

        if (day > 99) return "too long";

        coopDurationAsString += day > 0 ? $"{day}d" : "";
        coopDurationAsString += hour > 0 ? $"{hour}h" : "";
        coopDurationAsString += min > 0 ? $"{min}m" : "";

        return coopDurationAsString;
    }

    private void requestCoopStatus()
    {
        coopStatus = Request.GetCoopStatus(ContractId(), CoopId()).Result;
    }
}
