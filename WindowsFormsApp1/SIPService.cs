
using Ozeki.Media;
using Ozeki.VoIP;
using System;
using SIPWindowsAgent;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Nancy;
using Nancy.Helpers;
using onvif.services;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;
using AForge.Imaging.Filters;
using Ozeki;
using System.Reflection;
using System.Threading;
namespace sipservice
{
    public class SIPService
    {
        private MainForm form;
        private const string OzekiLicenseKey = "UDoyMDMzLTEyLTI1LFVQOjIwMzMtMDEtMDEsTUNDOjUwMCxNUEw6NTAwLE1TTEM6NTAwLE1GQzo1MDAsRzcyOTp0cnVlLE1XUEM6NTAwLE1JUEM6NTAwfHFQZDBhQnhlaEFGaTlNMmV4cXZxaHUyVE5rMWh2S0FzaUZlVlowbFFseTZWZ3JKbmFMTXh3ZVV2elBGcEliTFpwNHZtZDArZlZwc2VkRGpjQWdKR3ZnPT0=";
        private const string OzekiLicenseUserName = "OZSDK-CALL-1234567-IWAREZ 2017";
        private static ISoftPhone softphone;
        internal IPhoneLine phoneLine;
        internal List<IPhoneLine> phonLines = new List<IPhoneLine>();
        private IPhoneCall call1;
        private IPhoneCall call2;
        IPhoneCall ActualCall;
        static Speaker speaker;
        static MediaConnector connector;
        static PhoneCallAudioReceiver mediaReceiver;
        private int callDurationSeconds;
        static ConferenceRoom _conferenceRoom;
        static Microphone microphone;
        static PhoneCallAudioSender mediaSender;
        public int MinPort = 20001;
        public int MaxPort = 20500;
        private string bearerToken;
        List<CallerData> CallerInfo;
        private System.Timers.Timer callTimer;
        static int port = 10000;
        private bool flage;
        private IncomingCallForm incomingCallForm;
        private OutgoingCallForm OutGoingCall;
        private int NumberOfTries;
        private int NumberOfTries2;
        private readonly ApiServiceHelper _apiHelper;
        public SIPService(MainForm form, ApiServiceHelper apiHelper)
        {
            _apiHelper = apiHelper;
            try
            {
                this.form = form;
                Ozeki.Common.LicenseManager.Instance.SetLicense(OzekiLicenseUserName, OzekiLicenseKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RegisterAccount(string userName, string displayName, string authenticationId, string registerPassword, string domainHost, int domainPort)
        {
            softphone = SoftPhoneFactory.CreateSoftPhone(MinPort, MaxPort);

            flage = false;
            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();
            try
            {
                var registrationRequired = true;
                var account = new SIPAccount(registrationRequired, displayName, userName, authenticationId, registerPassword, domainHost, domainPort);

                phoneLine = softphone.CreatePhoneLine(account);

                phonLines.Add(phoneLine);// = softphone.CreatePhoneLine(account);
                phoneLine.RegistrationStateChanged += line_RegStateChanged;

                Log($"Registration For {userName} is in progress...", "MainForm");
                phoneLine.Config.KeepAliveInterval = 10;

                if (softphone != null)
                {
                    softphone.IncomingCall -= (Softphone_IncomingCall);
                    // softphone.Close();
                }
                softphone.IncomingCall += Softphone_IncomingCall;

                softphone.RegisterPhoneLine(phoneLine);
                InitializeConferenceRoom();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during SIP registration: " + ex.Message);
                Log($"Error during SIP registration for {userName}: {ex.Message}", "MainForm");
            }

        }
        public void InitializeConferenceRoom()
        {
            _conferenceRoom = new ConferenceRoom();
            _conferenceRoom.StartConferencing();
        }
        private void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {
            IPhoneLine currentPhoneLine = sender as IPhoneLine;
            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
            {
                Log($"Registration failed! for {currentPhoneLine.SIPAccount.RegisterName}, Reg State:{e.State}", "MainForm");
            }

            if (e.State == RegState.RegistrationSucceeded)
            {
                Log($"Registration succeeded for {currentPhoneLine.SIPAccount.RegisterName} - Online!", "MainForm");
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => form.registerCheckBox.Checked = true));
                }
                form.registerCheckBox.Checked = true;
            }
        }
        private void Softphone_IncomingCall(object sender, Ozeki.Media.VoIPEventArgs<IPhoneCall> e)
        {
            //e.Item
            Type type = e.Item.GetType();
            PropertyInfo property = type.GetProperty("PhoneLine");
            IPhoneLine currentPhoneLine = (IPhoneLine)property.GetValue(e.Item, null);
            if (form.InvokeRequired)
            {
                form.BeginInvoke(new Action(() => Softphone_IncomingCall(sender, e)));
                return;
            }
            string DialedNumber = e.Item.DialInfo.Dialed;
            call1 = e.Item;
            ActualCall = call1;
            string CallerId = ((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller;
            // Check if CallerId starts with "9"
            CallerId = FixIncomingNumber(CallerId);

            Log($"recived call from {CallerId} for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");
            call1.CallStateChanged += Call_CallStateChanged;
            form.getRightSettingForIncominCall(DialedNumber);

            string userToken = form.userToken;
            if (!string.IsNullOrEmpty(userToken))
            {
                string Token = userToken.ToString();
                var payload = new
                {
                    CallerId
                };
                CallerInfo = _apiHelper.MakeApiCall<List<CallerData>>(form.BarsaAddress, "GetCallerInfo", payload, Token).Result;

                //CallerInfo = await CallGetCallerInfoApi(userToken, CallerID);
            }
            incomingCallForm = new IncomingCallForm(CallerId, CallerInfo, this, form.userToken);
            ShowNotifyWindow(incomingCallForm);

            if (form.IsTransferEnabled && form.CouplePhone != CallerId)
            {
                call1.Answer();
                StartOutgoingCalls(form.CouplePhone);
                Log($"Transfer call to .. {form.CouplePhone} for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");
            }
        }

        public static void ShowNotifyWindow(Form form)
        {
            form.Show();
            form.BringToFront();
            form.TopMost = true;
        }

        private static string FixIncomingNumber(string CallerId)
        {
            if (CallerId.StartsWith("98"))
            {
                return "0" + CallerId.Substring(2);
            }
            if (CallerId.StartsWith("9"))
            {
                return "0" + CallerId;
            }
            return CallerId;
        }

        public void StartOutgoingCalls(string targetNumber)
        {

            call2 = softphone.CreateCallObject(phoneLine, targetNumber);
            ActualCall = call2;
            call2.CallStateChanged += OutgoingCallStateChanged;
            Log($"Ringing phone number {call1.DialInfo.Dialed} from {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
            call2.Start();
        }
        void OutgoingCallStateChanged(object sender, CallStateChangedArgs e)
        {
            var call = (ICall)sender;
            Log($"out going Call state: {e.State}.", "MainForm");
            if (e.State == CallState.Answered)
            {
                //incomingCallForm.UpdateLabelText($"Second Device Answered And Added To Confrerence");
                _conferenceRoom.AddToConference(call2);
                Log($"{call.DialInfo.Dialed} added to confrance from {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
            }
            if (e.State == CallState.LocalHeld)
            {
                Console.WriteLine("CallState.LocalHeld");
            }
            if (e.State.IsCallEnded())
            {
                Console.WriteLine("couple phone hanged up");
                call.HangUp();
            }
        }
        private void ConfrenceDisconected(object sender, VoIPEventArgs<ICall> e)
        {
            Log($"confence disconnected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller} for {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
            RejectCall();
        }
        private void ConfrenceConected(object sender, VoIPEventArgs<ICall> e)
        {
            Log($"confence connected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller} for {phoneLine.SIPAccount.RegisterName} Account", "MainForm");
        }
        public void CreateCall(string targetNumber)
        {
            var dialParams = new DialParameters(targetNumber);
            dialParams.CallType = CallType.Audio;
            if (phoneLine == null)
                MessageBox.Show($"First Register Your Sip Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {


                call1 = softphone.CreateCallObject(phoneLine, dialParams);
                call1.CallStateChanged += Call_CallStateChanged;
                call1.Start();
            }
        }
        private void Call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            Type type = sender.GetType();
            PropertyInfo property = type.GetProperty("PhoneLine");
            IPhoneLine currentPhoneLine = (IPhoneLine)property.GetValue(sender, null);
            Console.WriteLine("Call state: {0}.", e.State);
            Log($"Call state: {e.State}. for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");
            IPhoneCall CurrentCall = (IPhoneCall)sender;
            if (e.State == CallState.Answered)
            {
                _conferenceRoom.AddToConference(call1);
                if (!form.IsTransferEnabled)
                    SetupDevices();
                else if (form.IsTransferEnabled && !CurrentCall.IsIncoming && form.CouplePhone != CurrentCall.DialInfo.CallerID)
                {
                    StartOutgoingCalls(form.CouplePhone);
                    Log($"Transfer call to .. {form.CouplePhone} for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");

                }
                StartCallTimer();
                _conferenceRoom.CallConnected += ConfrenceConected;
                _conferenceRoom.CallDisconnected += ConfrenceDisconected;
                if (incomingCallForm != null && incomingCallForm.IsHandleCreated)
                {
                    // Check if the form's handle has been created
                    incomingCallForm.Invoke(new Action(() =>
                    {
                        incomingCallForm.btnAnswer.Enabled = false;
                    }));
                }

            }
            if (e.State == CallState.InCall)
            {

            }
            if (e.State == CallState.LocalHeld)
            {

            }
            if (e.State.IsCallEnded())
            {
                Log($"call completed for {currentPhoneLine.SIPAccount.RegisterName} duration was : {callDurationSeconds}", "MainForm");
                _conferenceRoom.CallConnected -= ConfrenceConected;
                _conferenceRoom.CallDisconnected -= ConfrenceDisconected;
                form.CallCompleted();
                if (incomingCallForm != null && incomingCallForm.IsHandleCreated)
                {
                    // Check if the form's handle has been created
                    incomingCallForm.Invoke(new Action(() =>
                    {
                        incomingCallForm.btnAnswer.Enabled = false;
                        incomingCallForm.btnDrop.Enabled = false;
                    }));
                }
                DropCall();
            }
        }
        void DisposeCall(IPhoneCall call)
        {
            if (call != null)
                call.CallStateChanged -= (Call_CallStateChanged);
            StopCallTimer();
        }
        private void SetupDevices()
        {
            try
            {
                _conferenceRoom.ConnectReceiver(speaker);
                _conferenceRoom.ConnectSender(microphone);

                //connector.Connect(microphone, mediaSender);
                //connector.Connect(mediaReceiver, speaker);

                mediaSender.AttachToCall(call1);
                mediaReceiver.AttachToCall(call1);

                if (!(microphone == null || speaker == null))
                {
                    speaker.Start();
                    microphone.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Setup Device Not Completed" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Log(string message, string FormType)
        {
            if (FormType == "MainForm")
            {
                form.UpdateLog(message);
            }
            if (FormType == "IncomingCallForm")//&& incomingCallForm?.ctlCallInfoList?.txtLog != null)
            {
                incomingCallForm.UpdateLabelText(message);
            }
            if (FormType == "OutGoingCallForm")// && OutGoingCall?.ctlCallInfoList?.txtLog != null)
            {
                incomingCallForm.UpdateLabelText(message);
            }
        }
        internal void AnswerCall()
        {
            call1.Answer();
        }
        internal void RejectCall()
        {
            if (call1 != null)
            {
                call1.HangUp();
                DisposeCall(call2);
            }
            StopCallTimer();
            if (call2 != null)
            {
                call2.HangUp();
                DisposeCall(call2);
            }
        }
        internal void DropCall()
        {
            if (call1 != null)
            {
                call1.HangUp();
                DisposeCall(call2);
            }
            StopCallTimer();
            if (call2 != null)
            {
                call2.HangUp();
                DisposeCall(call2);
            }
        }
        public void StartCallTimer()
        {

            callTimer = new Timer();
            callTimer.Interval = 1000; // 1 second interval
            callTimer.Elapsed += CallTimer_Tick; // Use Elapsed event instead of Tick
            callTimer.AutoReset = true; // Set AutoReset to true to make it repeat
            callTimer.Start();
        }
        public void StopCallTimer()
        {
            if (callTimer != null)
            {
                callTimer.Elapsed -= CallTimer_Tick;
                callTimer.Stop();
                callDurationSeconds = default;
            }

        }
        public void CallTimer_Tick(object sender, EventArgs e)
        {

            callDurationSeconds++;
            TimeSpan duration = TimeSpan.FromSeconds(callDurationSeconds);
            form.UpdateTextBoxText(form.recivedCallTime, string.Format("{0:D2}:{1:D2}:{2:D2}", duration.Hours, duration.Minutes, duration.Seconds));
        }
        private string GetLocalIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            System.Net.IPAddress[] addresses = ipEntry.AddressList;
            System.Net.IPAddress localIpAddress = addresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return localIpAddress?.ToString() ?? "Unknown";
        }
        private void HandleApiError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void HandleException(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        internal void PutCallOnHold()
        {

            if (ActualCall.CallState == CallState.InCall)
                ActualCall.Hold();
        }
        internal void UnHoldCall()
        {
            if (ActualCall.CallState == CallState.LocalHeld)
                ActualCall.Unhold();
        }
        internal async Task<List<SipSettings>> GetSipSettingAsync(string userToken, string barsaAdderess)
        {
            string apiUrl = $"http://{barsaAdderess}/api2/incomingCall/0.1/";
            string localIpAddress = GetLocalIpAddress();

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("UserCode", userToken);
                client.DefaultRequestHeaders.Add("UserCode", userToken);
                client.BaseAddress = new Uri(apiUrl);
                try
                {

                    var payload = new
                    {
                        userToken
                    };


                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "GetSipSettings");
                    var stringContent = JsonConvert.SerializeObject(payload);
                    req.Content = new StringContent(stringContent, System.Text.UTF8Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.SendAsync(req).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<List<SipSettings>>(responseBody);
                        //AppConfig appConfig = new AppConfig();

                        //// Populate the SipSettings dictionary within AppConfig
                        //foreach (var sipSettings in sipSettingsList)
                        //{
                        //    // Assuming UserName as the key for the dictionary
                        //    appConfig.SipSettings.Add(sipSettings.UserName, sipSettings);
                        //}
                        ////var responseObj = JsonConvert.DeserializeAnonymousType(responseBody, new { output = false });

                        ////bool result = responseObj.output;
                    }
                    else
                    {
                        MessageBox.Show("Can Not Get Sip Setting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
        }
        internal async Task<TokenResponse> GetToken(string barsaUserName, string barsaPassword, string barsaAdderess)
        {
            string apiUrl = $"{barsaAdderess}/api2/incomingCall/0.1/";
            string method = "GetToken";
            try
            {
                var payload = new
                {
                    barsaUserName,
                    barsaPassword
                };
                var result = _apiHelper.MakeApiCall<TokenResponse>(apiUrl, method, payload, null).Result;

                if (result == default)
                {
                    return null;
                }
                else
                {
                    if (result.ErrorMessage != null)
                    {
                        MessageBox.Show(result.ErrorMessage);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;

        }
        //internal async Task<string> CallTokenAPI()
        //{
        //    string loginApiUrl = $"http://{form.BarsaAddress}/api/auth/serviceLogin3";

        //    var credentials = new
        //    {
        //        un = form.BarsaUser,
        //        pwd = form.BarsaPass,
        //    };
        //    var jsonBody = JsonConvert.SerializeObject(credentials);
        //    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        //    using (HttpClient client = new HttpClient())
        //    {
        //        try
        //        {
        //            HttpResponseMessage loginResponse = await client.PostAsync(loginApiUrl, content);

        //            if (loginResponse.IsSuccessStatusCode)
        //            {
        //                string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

        //                var loginResponseData = JsonConvert.DeserializeObject<LoginResponse>(loginResponseBody);

        //                if (loginResponseData.succeed)
        //                {
        //                    // Remove "Bearer" prefix if present
        //                    string cleanBearerToken = loginResponseData.data.token.StartsWith("bearer ", StringComparison.OrdinalIgnoreCase)
        //                        ? loginResponseData.data.token.Substring(7)
        //                        : loginResponseData.data.token;
        //                    //TODO: should remoe this for tird api ??
        //                    bearerToken = cleanBearerToken;

        //                    return cleanBearerToken;
        //                }
        //                else
        //                {
        //                    HandleApiError("Call Token API Error!");
        //                }
        //            }
        //            else
        //            {
        //                HandleApiError("Login API Error: " + loginResponse.StatusCode);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            HandleException("Exception during API call: " + ex.Message);
        //        }
        //        return string.Empty;
        //    }
        //}
        internal async Task<CallerData> CallGetCallerInfoApi(string userToken, string CallerID)
        {
            string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("UserCode", userToken);
                client.BaseAddress = new Uri(apiUrl);
                try
                {
                    var payload = new
                    {
                        CallerID
                    };
                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "GetCallerInfo");
                    var stringContent = JsonConvert.SerializeObject(payload);
                    req.Content = new StringContent(stringContent, System.Text.UTF8Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.SendAsync(req).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show("API Response: " + responseBody, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var callerResponse = JsonConvert.DeserializeObject<SIPWindowsAgent.CallerData>(responseBody);

                        return callerResponse;

                        //if (callerResponse?.CallerMO != null &&
                        //    callerResponse.CallerMO.SequenceEqual(new[] { "User Not exist!", "" }))
                        //{
                        //    // Informative message when user data is not found
                        //    return new CallerResponse { CallerMO = new[] { "User Not Found", "Unknown", "Unknown", "Unknown" } };
                        //}
                        //else if (callerResponse?.CallerMO != null)
                        //{
                        //    // Handle other cases when user data is found
                        //    // MessageBox.Show($"Caller Info: {string.Join(", ", callerResponse.CallerMO)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return callerResponse;
                        //}

                    }
                    else
                    {
                        HandleApiError("CallGetCallerInfoApi Error: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    HandleException("Exception during CallGetCallerInfoApi API call: " + ex.Message);
                }
            }
            return null;
            //string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/GetCallerInfo?CallerID={CallerID}";

            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

            //    try
            //    {
            //        HttpResponseMessage response = await client.PostAsync(apiUrl, null);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            string responseBody = await response.Content.ReadAsStringAsync();
            //            //MessageBox.Show("API Response: " + responseBody, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            var callerResponse = JsonConvert.DeserializeObject<SIPWindowsAgent.CallerData>(responseBody);

            //            return callerResponse;

            //            //if (callerResponse?.CallerMO != null &&
            //            //    callerResponse.CallerMO.SequenceEqual(new[] { "User Not exist!", "" }))
            //            //{
            //            //    // Informative message when user data is not found
            //            //    return new CallerResponse { CallerMO = new[] { "User Not Found", "Unknown", "Unknown", "Unknown" } };
            //            //}
            //            //else if (callerResponse?.CallerMO != null)
            //            //{
            //            //    // Handle other cases when user data is found
            //            //    // MessageBox.Show($"Caller Info: {string.Join(", ", callerResponse.CallerMO)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            //    return callerResponse;
            //            //}

            //        }
            //        else
            //        {
            //            HandleApiError("CallGetCallerInfoApi Error: " + response.StatusCode);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        HandleException("Exception during CallGetCallerInfoApi API call: " + ex.Message);
            //    }
            //}
            //return null;
        }
        private async Task<string> CallGetLoginCodeApi(string userToken, string ClientIP)
        {
            string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("UserCode", userToken);
                client.BaseAddress = new Uri(apiUrl);
                try
                {
                    var payload = new
                    {
                        clientIpAddress = ClientIP
                    };
                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "GetLoginCode");
                    var stringContent = JsonConvert.SerializeObject(payload);
                    req.Content = new StringContent(stringContent, System.Text.UTF8Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.SendAsync(req).Result;
                    //        string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/GetLoginCode?Token={Token}&clientIpAddress={ClientIP}";

                    //using (HttpClient client = new HttpClient())
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                    //    try
                    //    {
                    //        HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show("API Response: " + responseBody, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        JObject jsonResponse = JObject.Parse(responseBody);
                        string loginCode = jsonResponse["LoginCode"].ToString();
                        return loginCode;
                    }
                    else
                    {
                        HandleException("Cannot Get LoginCode!!");
                    }
                }
                catch (Exception ex)
                {
                    HandleException("Exception during API call: " + ex.Message);
                }
            }
            return null;
        }

        public delegate void OpenMethodDelegate(string callNumber, string objectId, string customScript, string userToken);

        internal async Task<bool> CallRedirectAPI(string callNumber, string objectId, string customScript, string userToken, bool reset = false)
        {
            if (reset)
            {
                NumberOfTries = 0;
            }
            //string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/";
            //string localIpAddress = GetLocalIpAddress();

            //using (HttpClient client = new HttpClient())
            //{
            //client.DefaultRequestHeaders.Add("UserCode", userToken);
            //client.BaseAddress = new Uri(apiUrl);
            try
            {

                var payload = new
                {
                    objectId = objectId != null ? objectId.ToString() : "0",
                    callNumber = callNumber != null ? callNumber : "0",
                    customScript = customScript != null ? customScript : ""
                };



                bool result = _apiHelper.MakeApiCall<bool>(form.BarsaAddress, "IncomingCallRedirection", payload, userToken).Result;


                //HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "IncomingCallRedirection");
                //var stringContent = JsonConvert.SerializeObject(payload);
                //req.Content = new StringContent(stringContent, System.Text.UTF8Encoding.UTF8, "application/json");

                // HttpResponseMessage response = client.SendAsync(req).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //string responseBody = await response.Content.ReadAsStringAsync();

                //var responseObj = JsonConvert.DeserializeAnonymousType(responseBody, new { output = false });

                //bool result = responseObj.output;

                var completedTask = await Task.Run(() => SynchronizationManager.Instance.WaitForFormShown(1500));

                if (completedTask == true)
                {
                    //Sucsses: form shown within the specified time
                    NumberOfTries = 0;
                    return true;
                }
                else
                {
                    if (NumberOfTries == 0)
                    {
                        ShowPassword PassForn = new ShowPassword(this);
                        PassForn.Show();
                        //string LoginCode = await CallGetLoginCodeApi(bearerToken, localIpAddress);
                        //string urlToOpen = $"http://{form.BarsaAddress}/api/openid/callback?provider=AgentLogin&LoginCode={LoginCode}&url={HttpUtility.UrlEncode($"http://{form.BarsaAddress}")}";
                        //Process.Start(urlToOpen);
                    }
                    NumberOfTries++;
                    await Task.Delay(500);
                    if (NumberOfTries <= 12)
                    {
                        _ = CallRedirectAPI(callNumber, objectId, customScript, form.userToken);
                    }

                    // Make another HTTP request
                    //response = await client.PostAsync(apiUrl, null);

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    responseBody = await response.Content.ReadAsStringAsync();
                    //    responseObj = JsonConvert.DeserializeAnonymousType(responseBody, new { output = false });
                    //    result = responseObj.output;
                    //    return result;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;


        }
        public async void RedirectAfterPass(string Password)
        {
            //string LocalToken;
            //string loginApiUrl = "http://" + form.BarsaAddress + "/api/auth/serviceLogin3";

            //var credentials = new
            //{
            //    un = form.BarsaUser,
            //    pwd = Password,
            //};
            //var jsonBody = JsonConvert.SerializeObject(credentials);
            //var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //using (HttpClient client = new HttpClient())
            //{
            try
            {
                //HttpResponseMessage loginResponse = await client.PostAsync(loginApiUrl, content);
                //var token = GetToken(form.BarsaUser, Password, form.BarsaAddress);

                //if (token != null)
                //{
                //string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

                //var loginResponseData = JsonConvert.DeserializeObject<LoginResponse>(loginResponseBody);

                //if (loginResponseData.succeed)
                //{
                //    // Remove "Bearer" prefix if present
                //    string cleanBearerToken = loginResponseData.data.token.StartsWith("bearer ", StringComparison.OrdinalIgnoreCase)
                //        ? loginResponseData.data.token.Substring(7)
                //        : loginResponseData.data.token;
                //    //TODO: should remoe this for tird api ??
                //    LocalToken = cleanBearerToken;
                string localIpAddress = GetLocalIpAddress();
                string Token = form.userToken;
                var payload = new
                {
                    clientIpAddress = localIpAddress
                };
                LoginResponse loginResponse = _apiHelper.MakeApiCall<LoginResponse>(form.BarsaAddress, "GetLoginCode", payload, Token).Result;

                //string LoginCode = await CallGetLoginCodeApi(form.userToken, localIpAddress);
                string urlToOpen = $"{form.BarsaAddress}api/openid/callback?provider=CallManagementLogin&LoginCode={loginResponse.LoginCode}&url={HttpUtility.UrlEncode($"{form.BarsaAddress}")}";

                Process.Start(urlToOpen);

                //  return cleanBearerToken;
            }
            catch
            {
                HandleApiError("Login API Error:");
            }
        }
    }
    public class TokenResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class LoginResponse
    {
        public string LoginCode { get; set; }
    }
}

