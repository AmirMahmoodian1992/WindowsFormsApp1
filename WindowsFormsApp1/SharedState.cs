using Ozeki.VoIP;
using System.Collections.Generic;
using System;

internal class SharedState
{
    private static SharedState instance;
    public static SharedState Instance
    {
        get
        {
            if (instance == null)
                instance = new SharedState();
            return instance;
        }
    }

    private Dictionary<string, CallState> callStates = new Dictionary<string, CallState>();

    public event EventHandler<StateChangedEventArgs> StateChanged;

    public void UpdateState(string callId, CallState newState)
    {
        callStates[callId] = newState;
        OnStateChanged(new StateChangedEventArgs(callId, newState));
    }

    private void OnStateChanged(StateChangedEventArgs e)
    {
        StateChanged?.Invoke(this, e);
    }

    public CallState GetState(string callId)
    {
        if (callStates.ContainsKey(callId))
            return callStates[callId];
        else
        {
            throw new KeyNotFoundException($"Call ID '{callId}' not found in the call states.");
        }

    }
}

public class StateChangedEventArgs : EventArgs
{
    public string CallId { get; }
    public CallState NewState { get; }

    public StateChangedEventArgs(string callId, CallState newState)
    {
        CallId = callId;
        NewState = newState;
    }
}
