using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using iiscommon.Utilities;
using System;
using System.Collections;
using System.IO;

public class PrefabManipulationLogger : MonoBehaviour
{
    private DateTime actionStartTime;

    private void LogActionStarted(string actionType)
    {
        actionStartTime = DateTime.Now;
        CentralLogManager.Instance.LogEvent($"{actionType} Started on {gameObject.name}");
    }

    private void LogActionEnded(string actionType)
    {
        TimeSpan duration = DateTime.Now - actionStartTime;
        CentralLogManager.Instance.LogEvent($"{actionType} Ended on {gameObject.name}. Duration: \n {duration.TotalSeconds}");
    }

    [ContextMenu("Log Move Started")]
    public void LogMoveStarted()
    {
        LogActionStarted("Move");
    }

    [ContextMenu("Log Move Ended")]
    public void LogMoveEnded()
    {
        LogActionEnded("Move");
    }

    [ContextMenu("Log Scale Started")]
    public void LogScaleStarted()
    {
        LogActionStarted("Scale");
    }

    [ContextMenu("Log Scale Ended")]
    public void LogScaleEnded()
    {
        LogActionEnded("Scale");
    }

    [ContextMenu("Log Rotate Started")]
    public void LogRotateStarted()
    {
        LogActionStarted("Rotate");
    }

    [ContextMenu("Log Rotate Ended")]
    public void LogRotateEnded()
    {
        LogActionEnded("Rotate");
    }
}
