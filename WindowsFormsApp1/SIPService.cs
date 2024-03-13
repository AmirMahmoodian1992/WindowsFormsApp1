
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



namespace sipservice
{
    public class SIPService
    {
        private MainForm form;
        private const string OzekiLicenseKey = "UDoyMDMzLTEyLTI1LFVQOjIwMzMtMDEtMDEsTUNDOjUwMCxNUEw6NTAwLE1TTEM6NTAwLE1GQzo1MDAsRzcyOTp0cnVlLE1XUEM6NTAwLE1JUEM6NTAwfHFQZDBhQnhlaEFGaTlNMmV4cXZxaHUyVE5rMWh2S0FzaUZlVlowbFFseTZWZ3JKbmFMTXh3ZVV2elBGcEliTFpwNHZtZDArZlZwc2VkRGpjQWdKR3ZnPT0=";
        private const string OzekiLicenseUserName = "OZSDK-CALL-1234567-IWAREZ 2017";
        private static ISoftPhone softphone;
        private IPhoneLine phoneLine;
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
        CallerData CallerInfo;
        private System.Timers.Timer callTimer;




        public SIPService(MainForm form)
        {
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
        static int port = 10000;
        private bool flage;
        private IncomingCallForm incomingCallForm;
        private OutGoingCallForm OutGoingCall;
        private int NumberOfTries;
        private int NumberOfTries2;

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
                phoneLine.RegistrationStateChanged += line_RegStateChanged;
                phoneLine.Config.KeepAliveInterval = 10;

                if (softphone != null)
                {
                    softphone.IncomingCall -= (softphone_IncomingCall);
                    // softphone.Close();
                }
                softphone.IncomingCall += softphone_IncomingCall;

                softphone.RegisterPhoneLine(phoneLine);
                InitializeConferenceRoom();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during SIP registration: " + ex.Message);
                Log("Error during SIP registration: " + ex.Message, "MainForm");
            }

        }
        public void InitializeConferenceRoom()
        {
            _conferenceRoom = new ConferenceRoom();
            _conferenceRoom.StartConferencing();
        }

        private void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {

            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
            {
                Log($"Registration failed!, Reg State:{e.State}", "MainForm");
            }

            if (e.State == RegState.RegistrationSucceeded)
            {
                Log($"Registration succeeded - Online!", "MainForm");
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => form.RegisterRadioButton.Checked = true));
                }
                form.RegisterRadioButton.Checked = true;
            }
        }
        internal async Task<string> CallTokenAPI()
        {
            string loginApiUrl = "http://" + form.BarsaAddress + "/api/auth/serviceLogin3";

            var credentials = new
            {
                un = form.BarsaUser,
                pwd = form.BarsaPass,
            };
            var jsonBody = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage loginResponse = await client.PostAsync(loginApiUrl, content);

                    if (loginResponse.IsSuccessStatusCode)
                    {
                        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

                        var loginResponseData = JsonConvert.DeserializeObject<LoginResponse>(loginResponseBody);

                        if (loginResponseData.succeed)
                        {
                            // Remove "Bearer" prefix if present
                            string cleanBearerToken = loginResponseData.data.token.StartsWith("bearer ", StringComparison.OrdinalIgnoreCase)
                                ? loginResponseData.data.token.Substring(7)
                                : loginResponseData.data.token;
                            //TODO: should remoe this for tird api ??
                            bearerToken = cleanBearerToken;

                            return cleanBearerToken;
                        }
                        else
                        {
                            HandleApiError("Login API Error: " + loginResponseBody);
                        }
                    }
                    else
                    {
                        HandleApiError("Login API Error: " + loginResponse.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    HandleException("Exception during API call: " + ex.Message);
                }
                return string.Empty;
            }
        }


        internal async Task<CallerData> CallGetCallerInfoApi(string Token, string CallerID)
        {
            string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/GetCallerInfo?CallerID={CallerID}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show("API Response: " + responseBody, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var callerResponse = JsonConvert.DeserializeObject<SIPWindowsAgent.InfoData>(responseBody);
                        return JsonConvert.DeserializeObject<CallerData>(callerResponse.CallerJson)  ;

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
        }
        private async Task<string> CallGetLoginCodeApi(string Token, string ClientIP)
        {
            string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/GetLoginCode?Token={Token}&clientIpAddress={ClientIP}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

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
                        HandleException("Exception during API call: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    HandleException("Exception during API call: " + ex.Message);
                }
            }
            return null;
        }
        private async void softphone_IncomingCall(object sender, Ozeki.Media.VoIPEventArgs<IPhoneCall> e)
        {
            if (form.InvokeRequired)
            {
                form.BeginInvoke(new Action(() => softphone_IncomingCall(sender, e)));
                return;
            }
            call1 = e.Item;
            ActualCall = call1;
            string CallerID = ((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller;
            Log($"recived call from {CallerID}", "MainForm");
            call1.CallStateChanged += call_CallStateChanged;

            string BarcaToken = await CallTokenAPI();
            if (!string.IsNullOrEmpty(BarcaToken))
            {
                CallerInfo = await CallGetCallerInfoApi(BarcaToken, CallerID);
            }
            incomingCallForm = new IncomingCallForm(CallerID, CallerInfo, this);
            incomingCallForm.Show();
            if (form.IsTransferEnabled && form.CouplePhone != CallerID)
            {
                call1.Answer();
                StartOutgoingCalls(form.CouplePhone);
                Log($"Transfer call to .. " + form.CouplePhone, "MainForm");
            }
        }
        public void StartOutgoingCalls(string targetNumber)
        {

            var numberToDial = targetNumber;
            call2 = softphone.CreateCallObject(phoneLine, numberToDial);
            ActualCall = call2;
            call2.CallStateChanged += OutgoingCallStateChanged;
            Log($"Ringing phone number {call1.DialInfo.Dialed}", "MainForm");
            call2.Start();
        }
        void OutgoingCallStateChanged(object sender, CallStateChangedArgs e)
        {
            var call = (ICall)sender;
            Log($"out going Call state: {e.State}.", "IncomingCallForm");
            if (e.State == CallState.Answered)
            {
                //incomingCallForm.UpdateLabelText($"Second Device Answered And Added To Confrerence");
                _conferenceRoom.AddToConference(call2);
                Log($"{call.DialInfo.Dialed} added to confrance", "MainForm");
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
            Log($"confence disconnected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller}", "MainForm");
            RejectCall();
        }

        private void ConfrenceConected(object sender, VoIPEventArgs<ICall> e)
        {
            Log($"confence connected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller}", "MainForm");
        }

        public void CreateCall(string targetNumber,CallerData callerData)
        {
            var dialParams = new DialParameters(targetNumber);
            dialParams.CallType = CallType.Audio;
            if (phoneLine == null)
                MessageBox.Show($"First Register Your Sip Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                OutGoingCall = new OutGoingCallForm(targetNumber, callerData, this);
                OutGoingCall.Show();
                call1 = softphone.CreateCallObject(phoneLine, dialParams);
                call1.CallStateChanged += call_CallStateChanged;
                call1.Start();
            }
        }
        private void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            Console.WriteLine("Call state: {0}.", e.State);
            Log($"Call state: {e.State}.", "MainForm");
            //var a =sender.GetType;

            IPhoneCall CurrentCall = (IPhoneCall)sender;
            if (e.State == CallState.Answered)
            {
                _conferenceRoom.AddToConference(call1);
                if (!form.IsTransferEnabled)
                    SetupDevices();
                else if (form.IsTransferEnabled && !CurrentCall.IsIncoming && form.CouplePhone != CurrentCall.DialInfo.CallerID)
                {
                    StartOutgoingCalls(form.CouplePhone);
                    Log($"Transfer call to .. " + form.CouplePhone, "MainForm");
                    
                }
                StartCallTimer();
                _conferenceRoom.CallConnected += ConfrenceConected;
                _conferenceRoom.CallDisconnected += ConfrenceDisconected;
            }
            if (e.State == CallState.InCall)
            {

            }
            if (e.State == CallState.LocalHeld)
            {

            }
            if (e.State.IsCallEnded())
            {
                Log("call completed duration was :" + callDurationSeconds, "MainForm");
                _conferenceRoom.CallConnected -= ConfrenceConected;
                _conferenceRoom.CallDisconnected -= ConfrenceDisconected;
                RejectCall();
            }
        }
        void DisposeCall(IPhoneCall call)
        {
            if (call != null)
                call.CallStateChanged -= (call_CallStateChanged);
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
            if (FormType == "IncomingCallForm" && incomingCallForm?.ctlCallInfo?.txtLog != null)
            {
                incomingCallForm.UpdateLabelText(message);
            }
            if (FormType == "OutGoingCallForm" && OutGoingCall?.ctlCallInfo?.txtLog != null)
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
            call1.HangUp();
            StopCallTimer();
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
        internal async Task<bool> CallRedirectAPI(string BarcaFormID, bool reset = false)
        {
            if (reset)
            {
                NumberOfTries = 0;
            }
            string apiUrl = $"http://{form.BarsaAddress}/api2/incomingCall/0.1/IncomingCallRedirection?input={BarcaFormID}";
            string localIpAddress = GetLocalIpAddress();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var responseObj = JsonConvert.DeserializeAnonymousType(responseBody, new { output = false });

                        bool result = responseObj.output;

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
                                ShowPassord PassForn = new ShowPassord(this);
                                PassForn.Show();
                                //string LoginCode = await CallGetLoginCodeApi(bearerToken, localIpAddress);
                                //string urlToOpen = $"http://{form.BarsaAddress}/api/openid/callback?provider=AgentLogin&LoginCode={LoginCode}&url={HttpUtility.UrlEncode($"http://{form.BarsaAddress}")}";
                                //Process.Start(urlToOpen);
                            }
                            NumberOfTries++;
                            await Task.Delay(500);
                            if (NumberOfTries <= 12)
                            {
                                _ = CallRedirectAPI(BarcaFormID);
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
                    else
                    {
                        MessageBox.Show("Caller User NotFound!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("Third API Error: " + response.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        public async void RedirectAfterPass(string Password)
        {
            string LocalToken;
            string loginApiUrl = "http://" + form.BarsaAddress + "/api/auth/serviceLogin3";

            var credentials = new
            {
                un = form.BarsaUser,
                pwd = Password,
            };
            var jsonBody = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage loginResponse = await client.PostAsync(loginApiUrl, content);

                    if (loginResponse.IsSuccessStatusCode)
                    {
                        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

                        var loginResponseData = JsonConvert.DeserializeObject<LoginResponse>(loginResponseBody);

                        if (loginResponseData.succeed)
                        {
                            // Remove "Bearer" prefix if present
                            string cleanBearerToken = loginResponseData.data.token.StartsWith("bearer ", StringComparison.OrdinalIgnoreCase)
                                ? loginResponseData.data.token.Substring(7)
                                : loginResponseData.data.token;
                            //TODO: should remoe this for tird api ??
                            LocalToken = cleanBearerToken;
                            string localIpAddress = GetLocalIpAddress();
                            string LoginCode = await CallGetLoginCodeApi(LocalToken, localIpAddress);
                            string urlToOpen = $"http://{form.BarsaAddress}/api/openid/callback?provider=AgentLogin&LoginCode={LoginCode}&url={HttpUtility.UrlEncode($"http://{form.BarsaAddress}")}";
                            Process.Start(urlToOpen);

                            //  return cleanBearerToken;
                        }
                        else
                        {
                            HandleApiError("Login API Error: " + loginResponseBody);
                        }
                    }
                    else
                    {
                        HandleApiError("Login API Error: " + loginResponse.StatusCode);
                    }
                
                // return string.Empty;


                
                }
                catch (Exception ex)
                {
                    HandleException("Exception during API call: " + ex.Message);
                }
            }
        }
        private string GetLocalIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addresses = ipEntry.AddressList;
            IPAddress localIpAddress = addresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
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
    }
}
