using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using iiscommon.Utilities;  // Add this to use LogManager
using System;  // Added to use DateTime

public class ObjectParentSetter : MonoBehaviour
{
    private GameObject[] stepParents;
    private List<GameObject> allObjects;
    private Interactable[][] toggleButtons;
    private List<GameObject>[] stepObjects;
    private int currentButtonIndex = 0;

    private LogManager logManager;  // Create LogManager instance
    private DateTime lastToggleTime; // Added to record last button toggle time

    private void Start()
    {
        logManager = new LogManager();
        logManager.Initialize("SingleToggleParentSetterLog", true); // Initialize LogManager

        GameObject parentObject = GameObject.Find("Object Manager");

        stepParents = new GameObject[15];
        toggleButtons = new Interactable[2][];
        stepObjects = new List<GameObject>[15];
        allObjects = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            toggleButtons[i] = new Interactable[15];
        }

        for (int i = 1; i <= 15; i++)
        {
            stepParents[i - 1] = parentObject.transform.Find("Step " + i).gameObject;
            stepObjects[i - 1] = new List<GameObject>();

            for (int j = 0; j < 2; j++)
            {
                GameObject setParent = GameObject.Find("Toggle Set " + (j + 1));
                toggleButtons[j][i - 1] = setParent.transform.Find("Button " + i).GetComponent<Interactable>();

                int step = i;
                int set = j;
                toggleButtons[j][i - 1].OnClick.AddListener(() => ToggleAction(step, set));
            }
        }

        for (int i = 1; i < 15; i++)
        {
            toggleButtons[1][i].gameObject.SetActive(false);
        }

        GameObject backButton = GameObject.Find("Back");
        Interactable backInteractable = backButton.GetComponent<Interactable>();
        backInteractable.OnClick.AddListener(BackAction);
    }

    public void AddNewObject(GameObject newObj)
    {
        allObjects.Add(newObj);
        logManager.WriteToLog("New object added: " + newObj.name);  // Log the new object
    }

    public void SetParent(int step)
    {
        if (step < 1 || step > 15)
        {
            Debug.LogError("Invalid step: " + step);
            return;
        }

        Transform stepParent = stepParents[step - 1].transform;

        foreach (GameObject obj in allObjects)
        {
            obj.transform.parent = stepParent;
            stepObjects[step - 1].Add(obj);
        }
        allObjects.Clear();

        stepParents[step - 1].SetActive(false);
        logManager.WriteToLog("Set parent for step " + step);  // Log the parent setting
    }

    public void ClearChildren(int step)
    {
        foreach (GameObject child in stepObjects[step - 1])
        {
            child.transform.parent = null;
            allObjects.Add(child);
        }

        stepObjects[step - 1].Clear();

        stepParents[step - 1].SetActive(true);
    }

    public void BackAction()
    {
        if (currentButtonIndex > 0)
        {
            // Save changes on the current step
            if (!toggleButtons[1][currentButtonIndex].IsToggled)
            {
                toggleButtons[1][currentButtonIndex].IsToggled = true;
                toggleButtons[0][currentButtonIndex].IsToggled = true;
                SetParent(currentButtonIndex + 1);
            }

            // Move to the previous step
            toggleButtons[1][currentButtonIndex].gameObject.SetActive(false);
            toggleButtons[1][currentButtonIndex - 1].gameObject.SetActive(true);
            toggleButtons[1][currentButtonIndex - 1].IsToggled = false;
            toggleButtons[0][currentButtonIndex - 1].IsToggled = false;
            ClearChildren(currentButtonIndex);
            currentButtonIndex--;
        }
        logManager.WriteToLog("Back action invoked");  // Log the back action
    }




    public void ToggleAction(int step, int set)
    {
        toggleButtons[set == 0 ? 1 : 0][step - 1].IsToggled = toggleButtons[set][step - 1].IsToggled;

        if (toggleButtons[set][step - 1].IsToggled)
        {
            SetParent(step);
            if (set == 1)
            {
                toggleButtons[set][step - 1].gameObject.SetActive(false);
                if (step < 15)
                {
                    toggleButtons[set][step].gameObject.SetActive(true);
                    toggleButtons[set][step].IsToggled = false;
                    toggleButtons[set == 0 ? 1 : 0][step].IsToggled = false;
                    ClearChildren(step + 1);
                }
                currentButtonIndex = step;
            }

            TimeSpan toggleDuration = DateTime.Now - lastToggleTime;
            lastToggleTime = DateTime.Now;

            logManager.WriteToLog($"Toggled button {step}. Set: {set}. State: {(toggleButtons[set][step - 1].IsToggled ? "On" : "Off")}. Duration since last toggle: {toggleDuration.TotalSeconds} seconds");
        }
        else
        {
            ClearChildren(step);
            if (set == 0)
            {
                toggleButtons[1][currentButtonIndex].gameObject.SetActive(false);
                toggleButtons[1][step - 1].gameObject.SetActive(true);
                currentButtonIndex = step - 1;
            }
        }
    }
}