using Ei;

namespace WHAL_Int.Maj;

public class ActiveContract
{
    public Contract Contract { get; init; }
    private List<Coop> coops = new List<Coop>();
    public IEnumerable<Coop> Coops => coops.AsEnumerable();

    public ActiveContract(Contract contract) => Contract = contract;

    public async Task<Coop> AddCoop(string coopCode)
    {
        CoopBuilder builder = new(Contract, coopCode);
        Coop coop = await builder.Build();
        coops.Add(coop);
        return coop;
    }

    public void OrderCoops()
    {
        coops = new List<Coop>(coops.OrderBy(x => x));
    }
}
