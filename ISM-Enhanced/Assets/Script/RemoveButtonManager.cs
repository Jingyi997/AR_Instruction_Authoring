using UnityEngine;
using iiscommon.Utilities;
using System.Collections.Generic;

public class RemoveButtonManager : MonoBehaviour
{
    private LogManager logManager;
    private List<GameObject> buttonList = new List<GameObject>();

    private void Start()
    {
        logManager = new LogManager();
        logManager.Initialize("RemoveButtonLog", true);
    }

    public void RegisterButton(GameObject button)
    {
        buttonList.Add(button);
        logManager.WriteToLog(button.name + " created");
    }

    public void UnregisterButton(GameObject button)
    {
        if (buttonList.Contains(button))
        {
            buttonList.Remove(button);
            logManager.WriteToLog(button.name + " removed");
        }
    }

    public void LogButtonClick(string buttonName)
    {
        logManager.WriteToLog(buttonName + " clicked");
    }
}
