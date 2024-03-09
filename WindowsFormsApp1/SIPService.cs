﻿
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



namespace sipservice
{
    public class SIPService
    {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_RESTORE = 9;

        private static ISoftPhone softphone;
        private IPhoneLine phoneLine;
        private IPhoneCall call1;
        private IPhoneCall call2;

        static Speaker speaker;
        static MediaConnector connector;
        static PhoneCallAudioReceiver mediaReceiver;

        private MainForm form;
        private System.Timers.Timer callTimer;
        private int callDurationSeconds;
        static ConferenceRoom _conferenceRoom;
        static Microphone microphone;
        static PhoneCallAudioSender mediaSender;
        int MinPort = 20001;
        int MaxPort = 20500;
        private string bearerToken;
        CallerResponse CallerInfo;


        public SIPService(MainForm form)
        {
            try
            {
                this.form = form;
                var userName = "OZSDK-CALL-1234567-IWAREZ 2017";
                var key =
                    "" +
                    "UDoyMDMzLTEyLTI1LFVQOjIwMzMtMDEtMDEsTUNDOjUwMCxNUEw6NTAwLE1TTEM6NTAwLE1GQzo1MDAsRzcyOTp0cnVlLE1XUEM6NTAwLE1JUEM6NTAwfHFQZDBhQnhlaEFGaTlNMmV4cXZxaHUyVE5rMWh2S0FzaUZlVlowbFFseTZWZ3JKbmFMTXh3ZVV2elBGcEliTFpwNHZtZDArZlZwc2VkRGpjQWdKR3ZnPT0=";
                Ozeki.Common.LicenseManager.Instance.SetLicense(userName, key);
            }
            catch { }
        }
        static int port = 10000;
        private bool flage;
        private IncomingCallForm incomingCallForm;

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

                //var ExpirationTime = phoneLine.Config.ExpirationTime;
                //var ExplicitIdentity = phoneLine.Config.ExplicitIdentity;
                //var KeepAliveMode = phoneLine.Config.KeepAliveMode;
                //var KeepAliveInterval = phoneLine.Config.KeepAliveInterval;
                //var RegisterBeforeExpires = phoneLine.Config.RegisterBeforeExpires;

                //phoneLine.Config.KeepAliveMode = KeepAliveMode.NONE;
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
                Log("Error during SIP registration: " + ex.Message);
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
                Console.WriteLine("Registration failed!");
                Log($"Registration failed!, Reg State:{e.State}");
            }

            if (e.State == RegState.RegistrationSucceeded)
            {
                Console.WriteLine("Registration succeeded - Online!");
                Log($"Registration succeeded - Online!");
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => form.RegisterRadioButton.Checked = true));
                }
                form.RegisterRadioButton.Checked = true;
            }
        }
        private async Task<string> CallTokenAPI()
        {
            string loginApiUrl = "http://localhost:4172/api/auth/serviceLogin3";

            var credentials = new
            {
                un = form.BarcaUsername.Text,
                pwd = form.BarcaPass.Text,
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
                            //should remoe this for tird api ??
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
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return string.Empty;
            }
        }


        private async Task<CallerResponse> CallSecondApi(string Token, string CallerID)
        {
            string apiUrl = "http://localhost:4172/api2/incomingCall/0.1/GetCallerInfo?CallerID=" + CallerID;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("API Response: " + responseBody, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var callerResponse = JsonConvert.DeserializeObject<CallerResponse>(responseBody);

                        if (callerResponse?.CallerMO != null)
                        {
                            MessageBox.Show($"Caller Info: {string.Join(", ", callerResponse.CallerMO)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return callerResponse;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Second API Error: " + response.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    HandleException("Exception during API call: " + ex.Message);
                }
            }
            return new CallerResponse();
        }
        private async Task<string> CallGetLoginCodeApi(string Token, string ClientIP)
        {
            string apiUrl = "http://localhost:4172/api2/incomingCall/0.1/GetLoginCode?Token=" + Token + "&clientIpAddress=" + ClientIP;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("API Response: " + responseBody, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        JObject jsonResponse = JObject.Parse(responseBody);
                        string loginCode = jsonResponse["LoginCode"].ToString();
                        return loginCode;
                    }
                    else
                    {
                        MessageBox.Show("CallGetLoginCodeApi API Error: " + response.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string CallerID = ((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller;
            Log($"recived call from {CallerID}");
            call1.CallStateChanged += call_CallStateChanged;

            string BarcaToken = await CallTokenAPI();
            if (!string.IsNullOrEmpty(BarcaToken))
            {
                CallerInfo = await CallSecondApi(BarcaToken, CallerID);
            }
            incomingCallForm = new IncomingCallForm(CallerInfo, this);
            incomingCallForm.Show();

            form.UpdateIncomingNumber(CallerID);

            if (CallerID != form.couplePhone.Text)
            {
                call1.Answer();
                StartOutgoingCalls(form.couplePhone.Text);
                Log($"making call to .. " + form.couplePhone.Text);
            }
        }
        public void StartOutgoingCalls(string targetNumber)
        {

            var numberToDial = targetNumber;
            call2 = softphone.CreateCallObject(phoneLine, numberToDial);

            call2.CallStateChanged += OutgoingCallStateChanged;

            Log($"Ringing phone number \"{call1.DialInfo.Dialed}\".");
            call2.Start();
        }
        void OutgoingCallStateChanged(object sender, CallStateChangedArgs e)
        {
            var call = (ICall)sender;
            Log($"out going Call state: {e.State}.");
            if (e.State == CallState.Answered)
            {
                incomingCallForm.UpdateLabelText($"Second Device Answered And Added To Confrerence");
                _conferenceRoom.AddToConference(call2);
                Log($"{call.DialInfo.Dialed} added to confrance");
                SetupDevices();
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
            Log($"confence disconnected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller}");

            call1.HangUp();
            call2.HangUp();
            DisposeCall(call1);
            DisposeCall(call2);
        }

        private void ConfrenceConected(object sender, VoIPEventArgs<ICall> e)
        {
            Log($"confence connected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller}");
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
                call1.CallStateChanged += call_CallStateChanged;
                call1.Start();
            }
        }
        private void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            Console.WriteLine("Call state: {0}.", e.State);
            Log($"Call state: {e.State}.");
            //var a =sender.GetType;

            if (e.State == CallState.Answered)
            {
                _conferenceRoom.AddToConference(call1);
                SetupDevices();
                StartCallTimer();
                _conferenceRoom.CallConnected += ConfrenceConected;
                _conferenceRoom.CallDisconnected += ConfrenceDisconected;
            }
            if (e.State == CallState.InCall)
            {
                //_conferenceRoom.AddToConference(call1);
                //SetupDevices();
                //StartCallTimer();
                //_conferenceRoom.CallConnected += ConfrenceConected;
                //_conferenceRoom.CallDisconnected += ConfrenceDisconected;
            }
            if (e.State.IsCallEnded())
            {
                Log("call completed duration was :" + callDurationSeconds);
                if (incomingCallForm != null)
                {
                    incomingCallForm.UpdateLabelText("call completed duration was :" + callDurationSeconds);
                }
                _conferenceRoom.CallConnected -= ConfrenceConected;
                _conferenceRoom.CallDisconnected -= ConfrenceDisconected;
                DisposeCall(call1);
            }
        }
        void DisposeCall(IPhoneCall call)
        {
            call.CallStateChanged -= (call_CallStateChanged);
            StopCallTimer();
        }
        private void SetupDevices()
        {
            //_conferenceRoom.ConnectReceiver(speaker);
            //_conferenceRoom.ConnectSender(microphone);

            connector.Connect(microphone, mediaSender);
            connector.Connect(mediaReceiver, speaker);

            mediaSender.AttachToCall(call1);
            mediaReceiver.AttachToCall(call1);
            try
            {
                if (!(microphone == null || speaker == null))
                {
                    speaker.Start();
                    microphone.Start();
                }
            }
            catch { }

        }
        private void Log(string message)
        {
            form.UpdateLog(message);
        }
        internal void AnswerCall()
        {
            call1.Answer();
        }
        internal void RejectCall()
        {
            call1.HangUp();
            StopCallTimer();
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
        internal async Task<bool> CallRedirectAPI(string BarcaFormID)
        {
            string apiUrl = "http://localhost:4172/api2/incomingCall/0.1/IncomingCallRedirection?input=" + BarcaFormID;
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
                        //MessageBox.Show("Third API Response: " + result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        var completedTask = await
                                Task.Run(() => SynchronizationManager.Instance.WaitForFormShown(5000));

                        if (completedTask == true)
                        {
                            //form shown within the specified time
                            return true;
                        }
                        else
                        {

                            string LoginCode = await CallGetLoginCodeApi(bearerToken, localIpAddress);
                            string urlToOpen = "http://localhost:4172/api/openid/callback?provider=AgentLogin&LoginCode=" + LoginCode;
                            System.Diagnostics.Process.Start(urlToOpen);

                            await Task.Delay(10000);

                            // Make another HTTP request
                            response = await client.PostAsync(apiUrl, null);

                            if (response.IsSuccessStatusCode)
                            {
                                responseBody = await response.Content.ReadAsStringAsync();
                                responseObj = JsonConvert.DeserializeAnonymousType(responseBody, new { output = false });
                                result = responseObj.output;

                                // Your additional logic here

                                return result;
                            }
                            else
                            {
                                // Handle HTTP request failure
                                return false;
                            }
                        }

                        //bool formShown = SynchronizationManager.Instance.WaitForFormShown(1000);
                        //if (formShown)
                        //{
                        //    return formShown;
                        //}
                        //else
                        //    return false;


                        //string browserProcessName = "chrome";

                        //Process[] processes = Process.GetProcessesByName(browserProcessName);

                        //if (processes.Length > 0)
                        //{
                        //    Bring the first instance of the browser to the front

                        //    foreach (Process p in processes)
                        //    {
                        //        if (p.MainWindowHandle != IntPtr.Zero)
                        //        {
                        //            ShowWindow(p.MainWindowHandle, SW_RESTORE);

                        //            SetForegroundWindow(p.MainWindowHandle);
                        //        }
                        //    }

                        //    IntPtr mainWindowHandle = processes[0].MainWindowHandle;

                        //    if (mainWindowHandle != IntPtr.Zero)
                        //    {
                        //        SetForegroundWindow(mainWindowHandle);
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("Unable to retrieve the browser window handle.");
                        //    }
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Browser process not found.");
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Third API Error: " + response.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        private string GetLocalIpAddress()
        {
            // Get the local machine's IP addresses
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addresses = ipEntry.AddressList;

            // Find the first IPv4 address (you may adjust this logic based on your requirements)
            IPAddress localIpAddress = addresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            return localIpAddress?.ToString() ?? "Unknown";
        }
        private void HandleApiError(string errorMessage)
        {
            // Log the error or display an appropriate message to the user
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleException(string errorMessage)
        {
            // Log the exception or display an appropriate message to the user
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}