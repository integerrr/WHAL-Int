using Ei;
using WHAL_Int.EggIncApi;

namespace WHAL_Int.Maj;

public class Coop
{
    private ContractCoopStatusResponse coopStatus;

    public Coop(ContractCoopStatusResponse response)
    {
        if (response.ResponseStatus != ContractCoopStatusResponse.Types.ResponseStatus.NoError)
        {
            throw new InvalidDataException("Cannot find coop, ResponseStatus = " + response.ResponseStatus);
        }
        coopStatus = response;
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

    private void requestCoopStatus()
    {
        coopStatus = Request.GetCoopStatus(ContractId(), CoopId()).Result;
    }
}
