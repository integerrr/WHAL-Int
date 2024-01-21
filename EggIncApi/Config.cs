namespace WHAL_Int.EggIncApi;

class Config
{
    public string EID { get; set; } = "";
    private const string EidTxtPath = "EID.txt";

    public static Config LoadConfig()
    {
        if (File.Exists(EidTxtPath))
        {
            string eid = File.ReadAllText(EidTxtPath);
            return new Config { EID = eid };
        }
        else
        {
            return new Config();
        }
    }
}