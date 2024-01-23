using Ei;
using WHAL_Int.EggIncApi;

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
    }
}
