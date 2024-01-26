using WHAL_Int.EggIncApi;

namespace WHAL_Int.src;
public class CoopBuilder
{
    private readonly string contractId;
    private readonly string coopCode;

    public CoopBuilder(string contractId, string coopCode)
    {
        this.contractId = contractId;
        this.coopCode = coopCode;
    }

    public async Task<Coop> Build()
    {
        var coopStatus = await Request.GetCoopStatus(contractId, coopCode);
        return new Coop(coopStatus);
    }
}
