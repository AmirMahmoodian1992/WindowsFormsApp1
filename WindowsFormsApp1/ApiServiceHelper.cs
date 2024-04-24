using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Net;

public class ApiServiceHelper
{
    public async Task<T> MakeApiCall<T>(string apiUrl, string method, object payload, string userToken)
    {
        using (var httpClient = new HttpClient())
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string Url = $"{apiUrl}/api2/incomingCall/0.1/{method}";

            try
            {
                var stringContent = JsonConvert.SerializeObject(payload);
                var req = new HttpRequestMessage(HttpMethod.Post, Url);
                req.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");
                if (userToken != null)
                {
                    httpClient.DefaultRequestHeaders.Add("UserCode", userToken);
                }
                HttpResponseMessage response = httpClient.SendAsync(req).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(responseBody))
                    {
                        MessageBox.Show("Empty response received from the API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return default(T);
                    }
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
                else
                {
                    MessageBox.Show($"Responce From {method} API Not Succeed: {response.ReasonPhrase}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
                return default(T);
            }
        }
    }
}