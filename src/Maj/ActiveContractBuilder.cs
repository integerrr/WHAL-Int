using Ei;
using WHAL_Int.EggIncApi;

namespace WHAL_Int.Maj;

public class ActiveContractBuilder
{
    private string contractId;

    public ActiveContractBuilder(string contractId) => this.contractId = contractId;

    public async Task<ActiveContract> Build()
    {
        var periodicalsResponse = await Request.GetPeriodicals();
        Contract contract =
            periodicalsResponse.Contracts.Contracts.SingleOrDefault(c => c.Identifier == contractId)
            ?? throw new InvalidDataException($"Contract ID invalid: {contractId}");
        return new ActiveContract(contract);
    }
}
