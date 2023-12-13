using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicCheck : MonoBehaviour
{
    void Start()
    {
        GetSelectedMicrophoneInfo();
    }

    void GetSelectedMicrophoneInfo()
    {
        // Get the name of the currently selected microphone
        string selectedMicrophone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

        if (selectedMicrophone != null)
        {
            Debug.Log("Currently selected microphone: " + selectedMicrophone);

            // You can use the selectedMicrophone variable to get more information about the microphone if needed
        }
        else
        {
            Debug.LogWarning("No microphone selected.");
            // Handle the case where no microphone is selected
        }
    }
}
