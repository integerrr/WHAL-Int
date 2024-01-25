using Ei;
using WHAL_Int.EggIncApi;
using WHAL_Int.Maj;

internal class Program
{
    public static void Main(string[] args)
    {
        if (string.IsNullOrEmpty(Config.EID))
        {
            Console.WriteLine("\"EID.txt\" not found in root directory, please create the file and only put your EID in the file.");
            return;
        }

        Console.WriteLine(Config.EID);

        Coop coop = new("panama-canal-2024","crying830");
        Console.WriteLine(coop.Duration());
        // var coop = Request.GetCoopStatus("waffle-2024", "garden227").Result;
        // Console.WriteLine(coop.SecondsRemaining);

    }
}
