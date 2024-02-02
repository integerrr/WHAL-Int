using Ei;
using WHAL_Int.Maj;

namespace WHAL_Int.Formatter;

public interface IContractFormatter
{
    string Format(ActiveContract activeContract);
}
