using Ei;
using WHAL_Int.Maj;

namespace WHAL_Int.src;

public class ActiveContract
{
    private Contract contract;
    private List<Coop> coops = new List<Coop>();
    public IEnumerable<Coop> Coops => coops.AsEnumerable();

    public ActiveContract(Contract contract) => this.contract = contract;

    public async Task<Coop> AddCoop(string coopCode)
    {
        CoopBuilder builder = new(contract.Identifier, coopCode);
        Coop coop = await builder.Build();
        coops.Add(coop);
        return coop;
    }
}
