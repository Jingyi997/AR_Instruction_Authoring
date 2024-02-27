using UnityEngine;
using UnityEngine.Video;
using Microsoft.MixedReality.Toolkit.UI;
using iiscommon.Utilities;
using System;

public class VideoControlButton : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Interactable interactable;

    private LogManager logManager;
    private DateTime videoStartTime;
    private TimeSpan totalVideoPlayDuration;

    private void Start()
    {
        // Initialize LogManager
        logManager = new LogManager();
        logManager.Initialize("VideoLog", true); // You may replace "VideoLog" with your preferred log filename

        totalVideoPlayDuration = TimeSpan.Zero;

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnded;
        }
    }

    private void OnVideoEnded(VideoPlayer source)
    {
        if (interactable != null)
        {
            interactable.SetToggled(false);
        }

        // Calculate the duration for which the video has been playing
        TimeSpan videoPlayDuration = DateTime.Now - videoStartTime;
        totalVideoPlayDuration += videoPlayDuration;

        // Write to log
        logManager.WriteToLog($"Video Stopped. Played for: {videoPlayDuration.TotalSeconds} seconds. Total play duration: {totalVideoPlayDuration.TotalSeconds} seconds");
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnded;
        }
    }

    public void OnClick()
    {
        if (videoPlayer != null && interactable != null)
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                interactable.SetToggled(false);

                // Calculate the duration for which the video has been playing
                TimeSpan videoPlayDuration = DateTime.Now - videoStartTime;
                totalVideoPlayDuration += videoPlayDuration;

                // Write to log
                logManager.WriteToLog($"Video Paused. Played for: {videoPlayDuration.TotalSeconds} seconds. Total play duration: {totalVideoPlayDuration.TotalSeconds} seconds");
            }
            else
            {
                videoPlayer.Play();
                videoStartTime = DateTime.Now;
                interactable.SetToggled(true);

                // Write to log
                logManager.WriteToLog("Video Started Playing.");
            }
        }
    }
}
