using System.Threading;
using System;
using WindowsFormsApp1;

public class SynchronizationManager
{
    private static readonly SynchronizationManager instance = new SynchronizationManager();
    private ManualResetEvent formShownEvent = new ManualResetEvent(false);
    private DateTime formShownTimestamp;

    private SynchronizationManager() { }

    public static SynchronizationManager Instance
    {
        get { return instance; }
    }

    public void SetFormShown()
    {
        // Invoke the operation on the UI thread
        if (MainForm.SingleFormInstance.InvokeRequired)
        {
            MainForm.SingleFormInstance.Invoke(new Action(() => SetFormShown()));
        }
        else
        {
            formShownEvent.Set();
            formShownTimestamp = DateTime.Now;
            formShownEvent.Reset();
        }
    }

    public bool WaitForFormShown(int timeoutMilliseconds)
    {
        bool signaled = formShownEvent.WaitOne(timeoutMilliseconds);
        if (signaled && DateTime.Now - formShownTimestamp > TimeSpan.FromMilliseconds(timeoutMilliseconds))
        {
            // Signal occurred after the timeout, treat it as if it didn't happen
            return false;
        }

        return signaled;
    }
}
