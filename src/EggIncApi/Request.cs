using System.Net;
using Ei;
using Google.Protobuf;

namespace WHAL_Int.EggIncApi;

public class Request
{
    private static BasicRequestInfo rInfo = new()
    {
        EiUserId = Config.EID,
        ClientVersion = Config.CLIENT_VERSION,
        Version = Config.VERSION,
        Build = Config.BUILD,
        Platform = Config.PLATFORM
    };

    private static Task<PeriodicalsResponse>? periodicals;

    public static async Task<ContractCoopStatusResponse> GetCoopStatus(string contractId, string coopId)
    {
        ContractCoopStatusRequest coopStatusRequest = new()
        {
            Rinfo = rInfo,
            ContractIdentifier = contractId,
            CoopIdentifier = coopId,
            UserId = Config.EID
        };

        return await makeEggIncApiRequest("coop_status", coopStatusRequest, ContractCoopStatusResponse.Parser.ParseFrom);
    }

    public static async Task<EggIncFirstContactResponse> GetFirstContact()
    {
        EggIncFirstContactRequest firstContactRequest = new()
        {
            UserId = Config.EID,
            ClientVersion = Config.CURRENT_CLIENT_VERSION
        };

        return await makeEggIncApiRequest("bot_first_contact", firstContactRequest, EggIncFirstContactResponse.Parser.ParseFrom, false);
    }

    public static async Task<PeriodicalsResponse> GetPeriodicals(bool refresh = false)
    {
        if (periodicals == null || refresh)
        {
            GetPeriodicalsRequest getPeriodicalsRequest = new()
            {
                UserId = Config.EID,
                CurrentClientVersion = Config.CURRENT_CLIENT_VERSION
            };

            return await makeEggIncApiRequest("get_periodicals", getPeriodicalsRequest, PeriodicalsResponse.Parser.ParseFrom);
        }
        return await periodicals;
    }

    private static async Task<T> makeEggIncApiRequest<T>(string endpoint, IMessage data, Func<ByteString, T> parseMethod, bool isAuthenticatedMsg = true)
    {
        byte[] bytes;
        using (var stream = new MemoryStream())
        {
            data.WriteTo(stream);
            bytes = stream.ToArray();
        }

        Dictionary<string, string> body = new Dictionary<string, string> { { "data", Convert.ToBase64String(bytes) } };

        string response = await postRequest(endpoint, new FormUrlEncodedContent(body));

        if (isAuthenticatedMsg)
        {
            AuthenticatedMessage authMsg = AuthenticatedMessage.Parser.ParseFrom(Convert.FromBase64String(response));
            return parseMethod(authMsg.Message);
        }
        else
        {
            return parseMethod(ByteString.CopyFrom(Convert.FromBase64String(response)));
        }

    }

    private static async Task<string> postRequest(string endpoint, FormUrlEncodedContent body)
    {
        using (var client = new HttpClient())
        {
            string url = $"https://www.auxbrain.com/ei/{endpoint}";
            var response = await client.PostAsync(url, body);
            return await response.Content.ReadAsStringAsync();
        }
    }

    /*
        =========================================================================
        Below is simply additionals that can be retrieved from the above requests
        =========================================================================
    */

    public static Google.Protobuf.Collections.RepeatedField<Contract> GetContracts() => GetPeriodicals().Result.Contracts.Contracts;
    public static Contract GetContract(string contractId) {
        var contracts = GetContracts();

        string[] contractIds = contracts
            .Select(c => c.Identifier)
            .ToArray();

        int targetContractIndex = Array.IndexOf(contractIds,contractId);
        return contracts[targetContractIndex];
    }
}
