using UnityEngine;
using System.Collections.Generic;
using iiscommon.Utilities;

public class ObjectCreationManager : MonoBehaviour
{
    public ObjectCreationManager creationManager;
    private List<GameObject> createdObjects = new List<GameObject>();
    private LogManager logManager;

    void Start()
    {
        logManager = new LogManager();
        logManager.Initialize("ObjectCreationLog", true);
    }

    public void AddObjectToList(GameObject obj)
    {
        createdObjects.Add(obj);
        logManager.WriteToLog("New object created: " + obj.name);
    }

}
