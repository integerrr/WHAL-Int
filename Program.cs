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

        Console.OutputEncoding = System.Text.Encoding.Unicode;

        srucTable sruc = new srucTable("spring-2019", "intact860", "evolve046", "mortar845", "roller678", "mosaic076");
        Console.WriteLine(sruc.GetSrucTable());

    }
}
