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
//                var call = _softPhone.CreateCall(member);
//                call.CallStateChanged += OutgoingCallStateChanged;
//                _calls.Add(call);
//            }

//            lock (_sync)
//            {
//                foreach (var call in _calls)
//                {
//                    Console.WriteLine("Ringing phone number \"{0}\".", call.DialInfo.Dialed);
//                    call.Start();
//                }
//            }
//        }

//        void OutgoingCallStateChanged(object sender, CallStateChangedArgs e)
//        {
//            var call = (IPhoneCall)sender;

//            if (e.State == CallState.Answered)
//            {
//                Console.WriteLine("\nCall has been accepted by {0}.", call.DialInfo.Dialed);
//                Console.WriteLine("Call from \"{0}\" is being transferred to \"{1}\".\n", _incomingCall.DialInfo.Dialed, call.DialInfo.Dialed);
//                _incomingCall.AttendedTransfer(call);

//                lock (_sync)
//                {
//                    _calls.Remove(call);
//                    OnCompleted();
//                }
//            }

//            if (e.State == CallState.Busy || e.State == CallState.Error)
//            {
//                lock (_sync)
//                {
//                    call.HangUp();
//                    _calls.Remove(call);
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
//            foreach (var call in _calls)
//            {
//                Console.WriteLine("Ringing phone number \"{0}\" ends.", call.DialInfo.Dialed);
//                call.HangUp();
//            }
//            _calls.Clear();
//        }
//    }
//}
