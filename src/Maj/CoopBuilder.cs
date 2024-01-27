using Ei;
using WHAL_Int.EggIncApi;

namespace WHAL_Int.Maj;
public class CoopBuilder
{
    private readonly Contract contract;
    private readonly string coopCode;

    public CoopBuilder(Contract contract, string coopCode)
    {
        this.contract = contract;
        this.coopCode = coopCode;
    }

    public async Task<Coop> Build()
    {
        var coopStatus = await Request.GetCoopStatus(contract.Identifier, coopCode);
        return new Coop(coopStatus, contract);
    }
}
