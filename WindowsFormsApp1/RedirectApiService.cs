//using System.Threading.Tasks;
//using System.Windows.Forms;

//public class RedirectApiService
//{
//    private readonly ApiServiceHelper _apiHelper;

//    public RedirectApiService(ApiServiceHelper apiHelper)
//    {
//        _apiHelper = apiHelper;
//    }

//    public async Task<bool> CallRedirectAPI(string callNumber, string objectId, string customScript, string userToken, bool reset = false)
//    {
//        string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/IncomingCallRedirection";

//        var payload = new
//        {
//            objectId = objectId ?? "0",
//            callNumber = callNumber ?? "0",
//            customScript = customScript ?? "",
//            userToken
//        };

//        // Use helper method to make API call
//        var result = await _apiHelper.MakeApiCall<bool>(apiUrl, payload);

//        // Additional processing if needed

//        return result;
//    }
//}

//// Similar implementation for other API services
