using UnityEngine;
using UnityEngine.Video;
using Microsoft.MixedReality.Toolkit.UI;
using iiscommon.Utilities; // Import LogManager namespace
using System;

public class VideoControlSlider : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public PinchSlider pinchSlider;

    private bool isInteracting = false;
    private LogManager logManager; // LogManager object
    private DateTime interactionStartTime; // Records start time of interaction
    private TimeSpan totalInteractionDuration; // Records total interaction duration

    void Start()
    {
        logManager = new LogManager();
        logManager.Initialize("SliderInteractionLog", true); // Initializing LogManager
        totalInteractionDuration = TimeSpan.Zero; // Initialize total interaction duration

        // Add event listeners for OnInteractionStarted and OnInteractionEnded events
        pinchSlider.OnInteractionStarted.AddListener((_) =>
        {
            isInteracting = true;
            interactionStartTime = DateTime.Now; // Record start time of interaction
            logManager.WriteToLog("Interaction started"); // Log start of interaction
        });

        pinchSlider.OnInteractionEnded.AddListener((_) =>
        {
            isInteracting = false;
            DateTime interactionEndTime = DateTime.Now; // Record end time of interaction
            TimeSpan interactionDuration = interactionEndTime - interactionStartTime; // Calculate duration
            totalInteractionDuration += interactionDuration; // Add current interaction duration to total
            logManager.WriteToLog($"Interaction ended, duration: {interactionDuration.TotalSeconds} seconds, total duration: {totalInteractionDuration.TotalSeconds} seconds"); // Log end of interaction, duration, and total duration
        });
    }

    void Update()
    {
        if (videoPlayer != null && pinchSlider != null)
        {
            // Only update the video's frame when the user is interacting with the slider
            if (isInteracting)
            {
                long frame = (long)(videoPlayer.frameCount * pinchSlider.SliderValue);
                videoPlayer.frame = frame;
            }
            // Update the slider's value to reflect the video's current progress
            else
            {
                pinchSlider.SliderValue = (float)videoPlayer.frame / videoPlayer.frameCount;
            }
        }
    }

    private void OnDestroy()
    {
        // Remember to remove the listeners when the object is destroyed
        pinchSlider.OnInteractionStarted.RemoveListener((_) => isInteracting = true);
        pinchSlider.OnInteractionEnded.RemoveListener((_) => isInteracting = false);
    }
}
