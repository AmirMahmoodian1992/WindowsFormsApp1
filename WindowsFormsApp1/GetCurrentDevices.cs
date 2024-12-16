using Ozeki.Media;
using SIPWindowsAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;

public class GetCurrentDevices
{
    private AudioDeviceInfo speakerDevices;
    private AudioDeviceInfo microphoneDevices;
    public MediaHandlers MediaHandlers { get; private set; }


    public GetCurrentDevices(MediaHandlers mediaHandlers)
    {
        // Initialize the current default playback device name
        speakerDevices = GetAllPlaybackDeviceName();
        microphoneDevices = GetAllRecordingDeviceName();
        MediaHandlers = mediaHandlers;

        // Start a timer to periodically check for changes in the default playback device
        Timer timer = new Timer();
        timer.Interval = 1000; // Check every 1 second (adjust as needed)
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private AudioDeviceInfo GetAllPlaybackDeviceName()
    {
        foreach (var device in Speaker.GetDevices())
        {
            if (device.IsDefault)
            {
                return device;
            }
        }
        return Speaker.GetDevices().FirstOrDefault();
    }

    private AudioDeviceInfo GetAllRecordingDeviceName()
    {
        foreach (var device in Microphone.GetDevices())
        {
            if (device.IsDefault)
            {
                return device;
            }
        }
        return Microphone.GetDevices().FirstOrDefault();

    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        // Get the name of the current default playback device
        AudioDeviceInfo newSpeakerDevice = GetAllPlaybackDeviceName();
        AudioDeviceInfo newMicrophonDevice = GetAllRecordingDeviceName();
        if (newSpeakerDevice != null)
        {
            // Check if the default playback device has changed
            if  (speakerDevices == null || newSpeakerDevice.DeviceID != speakerDevices.DeviceID)
            {
                MediaHandlers.ChangeSpeaker(newSpeakerDevice); ;
            }
        }
        if (newMicrophonDevice != null)
        {
            if ( microphoneDevices == null || newMicrophonDevice.GUID != microphoneDevices.GUID)
            {
                MediaHandlers.ChangeMicrophone(newMicrophonDevice); ;
            }
        }
        speakerDevices = newSpeakerDevice;
        microphoneDevices = newMicrophonDevice;
    }
}
