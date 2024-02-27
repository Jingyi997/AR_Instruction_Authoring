using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using iiscommon.Utilities;
using System;
using System.Collections;
using System.IO;

public class CentralLogManager : MonoBehaviour
{
    public static CentralLogManager Instance; // Singleton instance

    private LogManager logManager;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        logManager = new LogManager();
        logManager.Initialize("CentralObjectManipulationLog", true);
    }

    public void LogEvent(string logMessage)
    {
        logManager.WriteToLog(logMessage);
    }
}
