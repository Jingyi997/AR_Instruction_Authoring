using UnityEngine;
using iiscommon.Utilities;
using System.Collections.Generic;

public class DotManager : MonoBehaviour
{
    private LogManager logManager;
    private List<GameObject> dotList = new List<GameObject>();

    private void Start()
    {
        logManager = new LogManager();
        logManager.Initialize("DotManagerLog", true);
    }

    public void RegisterDot(GameObject dot)
    {
        dotList.Add(dot);
        logManager.WriteToLog(dot.name + " created");
    }

    public void UnregisterDot(GameObject dot)
    {
        if (dotList.Contains(dot))
        {
            dotList.Remove(dot);
            logManager.WriteToLog(dot.name + " removed");
        }
    }

    public void LogMessage(string message)
    {
        logManager.WriteToLog(message);
    }
}

