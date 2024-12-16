
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
using utils;
using static System.Windows.Forms.AxHost;
using System.Runtime.CompilerServices;
using static utils.TAI.Calendar;
using Ozeki.Camera;
using DateTime = System.DateTime;
using System.Dynamic;
using Barsa.Spl;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;
namespace sipservice
{
    public class SIPService
    {
        private MainForm form;
        private const string OzekiLicenseKey = "UDoyMDMzLTEyLTI1LFVQOjIwMzMtMDEtMDEsTUNDOjUwMCxNUEw6NTAwLE1TTEM6NTAwLE1GQzo1MDAsRzcyOTp0cnVlLE1XUEM6NTAwLE1JUEM6NTAwfHFQZDBhQnhlaEFGaTlNMmV4cXZxaHUyVE5rMWh2S0FzaUZlVlowbFFseTZWZ3JKbmFMTXh3ZVV2elBGcEliTFpwNHZtZDArZlZwc2VkRGpjQWdKR3ZnPT0=";
        private const string OzekiLicenseUserName = "OZSDK-CALL-1234567-IWAREZ 2017";
        private static ISoftPhone softPhone;
        internal IPhoneLine phoneLine;
        internal List<IPhoneLine> phoneLines = new List<IPhoneLine>();
        private IPhoneCall callMain;
        private IPhoneCall callConfrance;
        private IPhoneCall callTransfer;


        internal IPhoneCall ActualCall;
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
        internal List<CallerData> CallerInfo;
        private System.Timers.Timer callTimer;
        static int port = 10000;
        private bool flage;
        private IncomingCallForm incomingCallForm;
        private OutgoingCallForm OutGoingCall;
        private int NumberOfTries;
        private int NumberOfTries2;
        private readonly ApiServiceHelper _apiHelper;
        public MediaHandlers MediaHandlers { get; private set; }
        private Dictionary<string, IncomingCallForm> incomingCallForms = new Dictionary<string, IncomingCallForm>();



        public SIPService(MainForm form, ApiServiceHelper apiHelper)
        {
            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();
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
            InitSoftphone();
            MediaHandlers = new MediaHandlers();
            MediaHandlers.Init();


        }
        internal void InitSoftphone()
        {
            if (softPhone != null)
            {
                foreach (var line in phoneLines)
                {
                    if (line.RegState == RegState.RegistrationSucceeded)
                        softPhone.UnregisterPhoneLine(line);
                }

                softPhone.IncomingCall -= (Softphone_IncomingCall);
                softPhone.Close();
            }
            softPhone = SoftPhoneFactory.CreateSoftPhone(MinPort, MaxPort);
        }
        public void Dispose()
        {
            // unregister phone lines
            foreach (IPhoneLine line in phoneLines)
            {
                if (line.RegState == RegState.RegistrationSucceeded)
                    softPhone.UnregisterPhoneLine(line);

                UnsubscribeFromLineEvents(line);
                line.Dispose();
            }
            phoneLines.Clear();
            softPhone.Close();

        }
        private void UnsubscribeFromLineEvents(IPhoneLine line)
        {
            if (line == null)
                return;

            line.RegistrationStateChanged -= line_RegStateChanged;
        }
        public void RegisterAccount(string userName, string displayName, string authenticationId, string registerPassword, string domainHost, int domainPort)
        {
            flage = false;
            try
            {
                var registrationRequired = true;
                var account = new SIPAccount(registrationRequired, displayName, userName, authenticationId, registerPassword, domainHost, domainPort);

                phoneLine = softPhone.CreatePhoneLine(account);

                phoneLines.Add(phoneLine);// = softPhone.CreatePhoneLine(account);
                phoneLine.RegistrationStateChanged += line_RegStateChanged;

                Logers($"Registration For {userName} is in progress...", "MainForm");
                phoneLine.Config.KeepAliveInterval = 10;

                if (softPhone != null)
                {
                    softPhone.IncomingCall -= (Softphone_IncomingCall);
                    // softPhone.Close();
                }
                softPhone.IncomingCall += Softphone_IncomingCall;

                softPhone.RegisterPhoneLine(phoneLine);
                InitializeConferenceRoom();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during SIP registration: " + ex.Message);
                Logers($"Error during SIP registration for {userName}: {ex.Message}", "MainForm");
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
                Logers($"Registration failed! for {currentPhoneLine.SIPAccount.RegisterName}, Reg State:{e.State}", "MainForm");
            }

            if (e.State == RegState.RegistrationSucceeded)
            {
                Logers($"Registration succeeded for {currentPhoneLine.SIPAccount.RegisterName} - Online!", "MainForm");
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => form.registerCheckBox.Checked = true));
                }
                form.registerCheckBox.Checked = true;
            }
        }

        private void Softphone_IncomingCall(object sender, Ozeki.Media.VoIPEventArgs<IPhoneCall> e)
        {
            lock (form._sync)
            {
                //e.Item
                Type type = e.Item.GetType();

                PropertyInfo property = type.GetProperty("PhoneLine");
                IPhoneLine currentPhoneLine = (IPhoneLine)property.GetValue(e.Item, null);
                //if (form.InvokeRequired)
                //{
                //    //var action = new Action(() => Softphone_IncomingCall(sender, e));
                //    Barsa.Spl.Async.RunAfterTime(1, form, () => Softphone_IncomingCall(sender, e));
                //    //form.BeginInvoke( action);
                //    return;
                //}
                string DialedNumber = e.Item.DialInfo.Dialed;
                if (callMain.CallState != CallState.InCall)
                    callMain = e.Item;
                ActualCall = callMain;
                string CallerId = ((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller;
                // Check if CallerId starts with "9"
                CallerId = FixIncomingNumber(CallerId);

                Logers($"recived callMain from {CallerId} for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");

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

                Barsa.Spl.Async.RunAfterTime(100, form, () =>
                {
                    incomingCallForm = new IncomingCallForm(CallerId, CallerInfo, this, form.userToken, e.Item.CallID, e.Item);
                    incomingCallForms[e.Item.CallID] = incomingCallForm;

                    ShowNotifyWindow(incomingCallForm);
                    

                });
               
                    callMain.CallStateChanged += Call_CallStateChanged;
                

                    if (form.IsTransferEnabled && form.CouplePhone != CallerId)
                {
                    callMain.Answer();
                    StartOutgoingCalls(form.CouplePhone);
                    Logers($"Transfer callMain to .. {form.CouplePhone} for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");
                }
            }

        }
        public IPhoneCall CreateCall(string targetNumber)
        {
            var dialParams = new DialParameters(targetNumber);
            dialParams.CallType = CallType.Audio;
            if (phoneLine == null || phoneLine.RegState != RegState.RegistrationSucceeded)
                MessageBox.Show($"First Register Your Sip Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                callMain = softPhone.CreateCallObject(phoneLine, dialParams);
                callMain.CallStateChanged += Call_CallStateChanged;
                ActualCall = callMain;
                callMain.Start();
                return callMain;
            }
            return null;
        }

        private void Call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            lock (form._sync)
            {
                Type type = sender.GetType();
                PropertyInfo property = type.GetProperty("PhoneLine");
                IPhoneLine currentPhoneLine = (IPhoneLine)property.GetValue(sender, null);
                Console.WriteLine("Call state: {0}.", e.State);
                Logers($"Call state: {e.State}. for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");
                IPhoneCall CurrentCall = (IPhoneCall)sender;
                SharedState.Instance.UpdateState(CurrentCall.CallID, e.State);

                CheckStopRingback();
                CheckStopRingtone();


                if (e.State == CallState.Setup)
                {
                    InsertLogViaApi(CurrentCall);

                }
                else
                {
                    UpdateLogViaApi(CurrentCall, duration: callDurationSeconds, customerInfo: JsonConvert.SerializeObject(CallerInfo).ToString(), log: JsonConvert.SerializeObject(form.txtLog.Text).ToString());
                }
                if (e.State.IsRinging())
                {
                    if (callMain.IsIncoming)
                        MediaHandlers.StartRingtone();
                    else
                        MediaHandlers.StartRingback();
                    return;
                }
                if (e.State == CallState.Answered)
                {
                    _conferenceRoom.AddToConference(callMain);

                    if (form.IsTransferEnabled && !CurrentCall.IsIncoming && form.CouplePhone != CurrentCall.DialInfo.CallerID)
                    {
                        StartOutgoingCalls(form.CouplePhone);
                        Logers($"Transfer callMain to .. {form.CouplePhone} for {currentPhoneLine.SIPAccount.RegisterName} Account", "MainForm");

                    }
                    StartCallTimer();
                    _conferenceRoom.CallConnected += ConfrenceConected;
                    _conferenceRoom.CallDisconnected += ConfrenceDisconected;
                    if (incomingCallForms.ContainsKey(CurrentCall.CallID))
                    {
                        incomingCallForm = incomingCallForms[CurrentCall.CallID];
                        // Do something with the incomingCallForm...

                        if (incomingCallForm != null && incomingCallForm.IsHandleCreated)
                        {
                            // Check if the form's handle has been created
                            incomingCallForm.Invoke(new Action(() =>
                            {
                                incomingCallForm.btnAnswer.Enabled = false;
                            }));
                        }
                    }


                }
                if (e.State.IsRemoteMediaCommunication())
                {
                    if (!form.IsTransferEnabled)
                    {
                        //SetupDevices();

                        if (callMain.Equals(CurrentCall))
                        {
                            MediaHandlers.AttachAudio(CurrentCall);
                            MediaHandlers.AttachVideo(CurrentCall);
                        }
                    }
                    return;
                }
                if (e.State == CallState.LocalHeld || e.State == CallState.InactiveHeld || e.State.IsCallEnded())
                {
                    if (callMain != null && callMain.Equals(CurrentCall))
                    {
                        MediaHandlers.DetachAudio();
                        MediaHandlers.DetachVideo();
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
                    if (CurrentCall.IsAnswered)
                    {
                        Logers($"callMain completed for {currentPhoneLine.SIPAccount.RegisterName} duration was : {callDurationSeconds}", "MainForm");
                    }
                    else
                    {
                        Logers($"callMain completed for {currentPhoneLine.SIPAccount.RegisterName}", "MainForm");
                    }
                    _conferenceRoom.CallConnected -= ConfrenceConected;
                    _conferenceRoom.CallDisconnected -= ConfrenceDisconected;
                    form.CallCompleted();
                    if (incomingCallForms.ContainsKey(CurrentCall.CallID))
                    {
                        incomingCallForm = incomingCallForms[CurrentCall.CallID];
                        if (incomingCallForm != null && incomingCallForm.IsHandleCreated)
                        {
                            // Check if the form's handle has been created
                            incomingCallForm.Invoke(new Action(() =>
                            {
                                incomingCallForm.btnAnswer.Enabled = false;
                                incomingCallForm.btnDrop.Enabled = false;
                            }));
                        }
                    }
                    DropCall(CurrentCall);
                }
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

            callConfrance = softPhone.CreateCallObject(phoneLine, targetNumber);
            ActualCall = callConfrance;
            callConfrance.CallStateChanged += OutgoingCallStateChanged;
            Logers($"Ringing phone number {callMain.DialInfo.Dialed} from {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
            callConfrance.Start();
        }
        void OutgoingCallStateChanged(object sender, CallStateChangedArgs e)
        {
            var call = (ICall)sender;
            Logers($"out going Call state: {e.State}.", "MainForm");
            if (e.State == CallState.Answered)
            {
                //incomingCallForm.UpdateLabelText($"Second Device Answered And Added To Confrerence");
                _conferenceRoom.AddToConference(callConfrance);
                Logers($"{call.DialInfo.Dialed} added to confrance from {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
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
        public void StartTransferCalls(string targetNumber)
        {

            callTransfer = softPhone.CreateCallObject(phoneLine, targetNumber);
            ActualCall = callTransfer;
            callTransfer.CallStateChanged += TransferCallStateChanged;
            Logers($"Ringing phone number {callMain.DialInfo.Dialed} from {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
            callTransfer.Start();
        }
        void TransferCallStateChanged(object sender, CallStateChangedArgs e)
        {
            var call = (ICall)sender;
            Logers($"out going Call state: {e.State}.", "MainForm");
            if (e.State == CallState.Answered)
            {
                callMain.AttendedTransfer(call);



                Logers($"{callMain.DialInfo.Dialed} Transfered For {phoneLine.SIPAccount.RegisterName} Account  to {call.CallID}", "MainForm");
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
            Logers($"confence disconnected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller} for {phoneLine.SIPAccount.RegisterName} Account ", "MainForm");
            RejectCall(ActualCall);
        }
        private void ConfrenceConected(object sender, VoIPEventArgs<ICall> e)
        {
            Logers($"confence connected from {((Ozeki.VoIP.LocalCallWrapper)e.Item).CallerIDAsCaller} for {phoneLine.SIPAccount.RegisterName} Account", "MainForm");
        }
        //public IPhoneCall CreateCall(string targetNumber)
        //{
        //    var dialParams = new DialParameters(targetNumber);
        //    dialParams.CallType = CallType.Audio;
        //    if (phoneLine == null || phoneLine.RegState != RegState.RegistrationSucceeded)
        //        MessageBox.Show($"First Register Your Sip Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    else
        //    {
        //        callMain = softPhone.CreateCallObject(phoneLine, dialParams);
        //        callMain.CallStateChanged += Call_CallStateChanged;
        //        ActualCall = callMain;
        //        callMain.Start();
        //        return callMain;
        //    }
        //    return null;
        //}

        private void UpdateLogViaApi(IPhoneCall currentCall,
                                     string date = default,
                                     int duration = default,
                                     string customerInfo = default,
                                     string log = default
                                     )
        {
            string Token = form.userToken;
            var payload = new
            {
                internalNumber = currentCall.DialInfo.CallerID,
                callerNumber = currentCall.DialInfo.Dialed,
                callType = currentCall.IsIncoming ? 0 : 1, // Convert boolean to integer,
                callResult = currentCall.IsAnswered ? 1 : 0,
                date = date ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                duration,
                customerInfo,
                log,
                callId = currentCall.CallID,
                status = currentCall.CallState.ToString(),

            };
            var insertLogApiResponse = _apiHelper.MakeApiCall<InsertLogApiResponse>(form.BarsaAddress, "UpdateLog", payload, Token).Result;
        }

        private void InsertLogViaApi(IPhoneCall currentCall)
        {
            string Token = form.userToken;
            var endTime = DateTime.Now; // Assuming the call ends when making the API call
            var payload = new
            {
                internalNumber = currentCall.DialInfo.CallerID,
                callerNumber = currentCall.DialInfo.Dialed,
                callType = currentCall.IsIncoming ? 0 : 1, // Convert boolean to integer,
                callResult = currentCall.IsAnswered ? 0 : 1,
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                duration = 0, // Convert duration to total seconds
                customerInfo = JsonConvert.SerializeObject(CallerInfo).ToString(),
                log = JsonConvert.SerializeObject(form.txtLog.Text).ToString(),
                callId = currentCall.CallID,
                status = currentCall.CallState.ToString(),
            };
            var insertLogApiResponse = _apiHelper.MakeApiCall<InsertLogApiResponse>(form.BarsaAddress, "InsertLog", payload, Token).Result;
        }

        void DisposeCall(IPhoneCall call)
        {
            if (call != null)
                call.CallStateChanged -= (Call_CallStateChanged);
            //StopCallTimer();
        }
        private void SetupDevices()
        {
            try
            {
                microphone = Microphone.GetDefaultDevice();
                speaker = Speaker.GetDefaultDevice();
                _conferenceRoom.ConnectReceiver(speaker);
                _conferenceRoom.ConnectSender(microphone);
                //connector.Connect(microphone, mediaSender);
                //connector.Connect(mediaReceiver, speaker);
                mediaSender.AttachToCall(callMain);
                mediaReceiver.AttachToCall(callMain);
                microphone?.Start();
                speaker?.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Setup Device Not Completed" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CheckStopRingback()
        {
            bool stopRinging = true;
            if (!callMain.IsIncoming && callMain.CallState.IsRinging())
            {
                stopRinging = false;
            }
            if (stopRinging)
                MediaHandlers.StopRingback();

        }

        private void CheckStopRingtone()
        {
            bool stopRinging = true;
            if (callMain.IsIncoming && callMain.CallState.IsRinging())
            {
                stopRinging = false;
            }
            if (stopRinging)
                MediaHandlers.StopRingtone();
        }

        private void Logers(string message, string FormType)
        {
            if (FormType == "MainForm")
            {
                form.UpdateLog(message);
            }
            if ((ActualCall != null))
            {
                if (incomingCallForms.ContainsKey(ActualCall.CallID))
                {
                    incomingCallForm = incomingCallForms[ActualCall.CallID];
                    if (FormType == "IncomingCallForm")//&& incomingCallForm?.ctlCallInfoList?.txtLog != null)
                    {

                        incomingCallForm.UpdateLabelText(message);
                    }
                    if (FormType == "OutGoingCallForm")// && outgoingCallForm?.ctlCallInfoList?.txtLog != null)
                    {
                        incomingCallForm.UpdateLabelText(message);
                    }
                }
            }
        }
        internal void AnswerCall(IPhoneCall call)
        {
            call.Answer();
        }

        internal void RejectCall(IPhoneCall call)
        {
            if (call != null)
            {
                call.HangUp();
                DisposeCall(call);
                if (call.IsAnswered)
                    StopCallTimer();
            }
            //if (callConfrance != null)
            //{
            //    callConfrance.HangUp();
            //    DisposeCall(callConfrance);
            //}
        }
        internal void DropCall(IPhoneCall call)
        {
            if (call != null)
            {
                call.HangUp();
                DisposeCall(call);
                if (call.IsAnswered)
                    StopCallTimer();
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
        internal TokenResponse GetToken(string barsaUserName, string barsaPassword, string barsaAdderess)
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
                    HandleException("Exception during CallGetCallerInfoApi API callMain: " + ex.Message);
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
            //        HandleException("Exception during CallGetCallerInfoApi API callMain: " + ex.Message);
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
                    HandleException("Exception during API callMain: " + ex.Message);
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
            try
            {
                var payload = new
                {
                    objectId = objectId != null ? objectId.ToString() : "0",
                    callNumber = callNumber != null ? callNumber : "0",
                    customScript = customScript != null ? customScript : ""
                };
                bool result = _apiHelper.MakeApiCall<RedirectApiResponse>(form.BarsaAddress, "IncomingCallRedirection", payload, userToken).Result.Output;
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
                        ShowPassword PassForm = new ShowPassword(this);
                        PassForm.Show();
                        PassForm.btnLogin.Click += async (sender, args) =>
                        {
                            await TryForShowingForm(callNumber, objectId, customScript);
                        };

                    }
                    else
                    {
                        await TryForShowingForm(callNumber, objectId, customScript);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private async Task TryForShowingForm(string callNumber, string objectId, string customScript)
        {
            NumberOfTries++;
            await Task.Delay(500);
            if (NumberOfTries <= 10)
            {
                _ = CallRedirectAPI(callNumber, objectId, customScript, form.userToken);
            }
        }

        public async void RedirectAfterPass(string password, string username)
        {
            try
            {
                string localIpAddress = GetLocalIpAddress();
                var payload = new
                {
                    barsaUserName = username,
                    barsaPassword = password,
                };

                TokenResponse tokenResponse = await _apiHelper.MakeApiCall<TokenResponse>(form.BarsaAddress, "GetToken", payload, null);
                if (tokenResponse != null)
                {
                    if (tokenResponse.Success)
                    {
                        string Token = tokenResponse.Token;
                        var payloadGetLoginCode = new
                        {
                            clientIpAddress = localIpAddress
                        };
                        LoginResponse loginResponse = _apiHelper.MakeApiCall<LoginResponse>(form.BarsaAddress, "GetLoginCode", payloadGetLoginCode, Token).Result;

                        string urlToOpen = $"{form.BarsaAddress}api/openid/callback?provider=CallManagementLogin&LoginCode={loginResponse.LoginCode}&url={HttpUtility.UrlEncode($"{form.BarsaAddress}")}";

                        Process.Start(urlToOpen);
                    }
                    else
                    {
                        MessageBox.Show(tokenResponse.ErrorMessage ?? "An error occurred during login.");
                    }
                }
                else
                {
                    MessageBox.Show("Error In Loging With Your User Pass");
                }

            }
            catch
            {
                HandleApiError("Login API Error:");
            }
        }

        internal void TransferCall(string numberToTransfer)
        {

            StartTransferCalls(numberToTransfer);
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
    public class RedirectApiResponse
    {
        public bool Output { get; set; }
    }
    public class InsertLogApiResponse
    {
        public long Result { get; set; }
    }
}

