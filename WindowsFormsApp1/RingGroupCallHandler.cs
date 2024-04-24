//using System;
//using System.Collections.Generic;
//using Ozeki.VoIP;

//namespace _11_RingGroup
//{
//    class RingGroupCallHandler
//    {
//        IPhoneCall _incomingCall;
//        SIPService _softPhone;
//        List<IPhoneCall> _calls;
//        List<string> _members;
//        object _sync;

//        public RingGroupCallHandler(IPhoneCall incomingCall, SIPService softPhone, List<string> members)
//        {
//            _sync = new object();
//            _incomingCall = incomingCall;
//            _softPhone = softPhone;
//            _members = members;

//            _calls = new List<IPhoneCall>();
//        }

//        public event EventHandler Completed;

//        public void Start()
//        {
//            _incomingCall.Answer();
//            _incomingCall.CallStateChanged += call_CallStateChanged;
//            StartOutgoingCalls();
//        }

//        void StartOutgoingCalls()
//        {
//            foreach (var member in _members)
//            {
//                var callMain = _softPhone.CreateCall(member);
//                callMain.CallStateChanged += OutgoingCallStateChanged;
//                _calls.Add(callMain);
//            }

//            lock (_sync)
//            {
//                foreach (var callMain in _calls)
//                {
//                    Console.WriteLine("Ringing phone number \"{0}\".", callMain.DialInfo.Dialed);
//                    callMain.Start();
//                }
//            }
//        }

//        void OutgoingCallStateChanged(object sender, CallStateChangedArgs e)
//        {
//            var callMain = (IPhoneCall)sender;

//            if (e.State == CallState.Answered)
//            {
//                Console.WriteLine("\nCall has been accepted by {0}.", callMain.DialInfo.Dialed);
//                Console.WriteLine("Call from \"{0}\" is being transferred to \"{1}\".\n", _incomingCall.DialInfo.Dialed, callMain.DialInfo.Dialed);
//                _incomingCall.AttendedTransfer(callMain);

//                lock (_sync)
//                {
//                    _calls.Remove(callMain);
//                    OnCompleted();
//                }
//            }

//            if (e.State == CallState.Busy || e.State == CallState.Error)
//            {
//                lock (_sync)
//                {
//                    callMain.HangUp();
//                    _calls.Remove(callMain);
//                    if (_calls.Count == 0)
//                    {
//                        Console.WriteLine("No available destination.");
//                        _incomingCall.HangUp();
//                    }
//                }
//            }
//        }

//        void call_CallStateChanged(object sender, CallStateChangedArgs e)
//        {
//            if (e.State.IsCallEnded())
//            {
//                OnCompleted();
//            }
//        }

//        void OnCompleted()
//        {
//            HangupOutgoingCalls();

//            var handler = Completed;
//            if (handler != null)
//                handler(this, EventArgs.Empty);
//        }

//        void HangupOutgoingCalls()
//        {
//            foreach (var callMain in _calls)
//            {
//                Console.WriteLine("Ringing phone number \"{0}\" ends.", callMain.DialInfo.Dialed);
//                callMain.HangUp();
//            }
//            _calls.Clear();
//        }
//    }
//}
