namespace WHAL_Int.EggIncApi;

public class Request
{

}

public static class BasicRequestInfoParameters
{
    public const uint CLIENT_VERSION = 62;
    public const string VERSION = "1.29.1";
    public const string BUILD = "111279";
    public const string PLATFORM = "IOS";
    public static string EID { get; set; } = "";
    
    static BasicRequestInfoParameters()
    {
        Config config = Config.LoadConfig();
        EID = config.EID;
    }
}
