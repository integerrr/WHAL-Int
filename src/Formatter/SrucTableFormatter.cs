using Ei;
using WHAL_Int.Maj;

namespace WHAL_Int.Formatter;

public class SrucTableFormatter : IContractFormatter
{
    public string Format(ActiveContract activeContract)
    {
        string str = "Total tokens: \n";
        foreach (var coop in activeContract.Coops)
        {
            str += $"{coop.CoopId}: {coop.TotalTokens} \n";
        }

        return str;
    }
}
