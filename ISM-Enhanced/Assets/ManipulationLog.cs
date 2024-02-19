using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using iiscommon.Utilities;
using System;
using System.Collections;
using System.IO;

public class ManipulationLog : MonoBehaviour
{
    private LogManager logManager;

    private DateTime moveStartTime;
    private DateTime scaleStartTime;
    private DateTime rotateStartTime;

    private TimeSpan totalMoveDuration = TimeSpan.Zero;
    private TimeSpan totalScaleDuration = TimeSpan.Zero;
    private TimeSpan totalRotateDuration = TimeSpan.Zero;

    private void Start()
    {
        logManager = new LogManager();
        logManager.Initialize("ObjectManipulationLog", true);
    }

    [ContextMenu("Log Move Started")]
    public void LogMoveStarted()
    {
        moveStartTime = DateTime.Now;
        logManager.WriteToLog("Move Started");
    }

    [ContextMenu("Log Move Ended")]
    public void LogMoveEnded()
    {
        TimeSpan duration = DateTime.Now - moveStartTime;
        totalMoveDuration += duration;

        logManager.WriteToLog($"Move Ended. Duration: {duration.TotalSeconds} seconds");
        logManager.WriteToLog($"Total Move Duration: {totalMoveDuration.TotalSeconds} seconds");
    }

    [ContextMenu("Log Scale Started")]
    public void LogScaleStarted()
    {
        scaleStartTime = DateTime.Now;
        logManager.WriteToLog("Scale Started");
    }

    [ContextMenu("Log Scale Ended")]
    public void LogScaleEnded()
    {
        TimeSpan duration = DateTime.Now - scaleStartTime;
        totalScaleDuration += duration;

        logManager.WriteToLog($"Scale Ended. Duration: {duration.TotalSeconds} seconds");
        logManager.WriteToLog($"Total Scale Duration: {totalScaleDuration.TotalSeconds} seconds");
    }

    [ContextMenu("Log Rotate Started")]
    public void LogRotateStarted()
    {
        rotateStartTime = DateTime.Now;
        logManager.WriteToLog("Rotate Started");
    }

    [ContextMenu("Log Rotate Ended")]
    public void LogRotateEnded()
    {
        TimeSpan duration = DateTime.Now - rotateStartTime;
        totalRotateDuration += duration;

        logManager.WriteToLog($"Rotate Ended. Duration: {duration.TotalSeconds} seconds");
        logManager.WriteToLog($"Total Rotate Duration: {totalRotateDuration.TotalSeconds} seconds");
    }
 
}
