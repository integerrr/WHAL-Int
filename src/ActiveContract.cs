using Ei;

namespace WHAL_Int.src;

public class ActiveContract
{
    private Contract contract;
    private List<Coop> coops = new List<Coop>();

    public ActiveContract(Contract contract) => this.contract = contract;

    public async Task<Coop> AddCoop(string coopCode)
    {
        CoopBuilder builder = new(contract.Identifier, coopCode);
        Coop coop = await builder.Build();
        coops.Add(coop);
        return coop;
    }
}
