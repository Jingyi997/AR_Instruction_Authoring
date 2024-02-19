using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using iiscommon.Utilities;
using System;
using System.Collections;
using System.IO;

public class SingleToggleParentSetter : MonoBehaviour
{
    public GameObject[] stepParents;
    public Interactable[] toggles;
    public Interactable backButton;
    public GameObject[] stamps; // Array for your stamps
    //public GameObject notificationPrefab;
    public Interactable goButton;
    public Interactable pauseButton;
    


    private List<GameObject> allObjects = new List<GameObject>();
    private bool[] isToggleOn;
    private int activeToggleIndex = 0;
    private bool isBackButtonClicked = false;
    private DateTime pauseOffTime;
    private bool isPaused = false;
    private DateTime pauseOnTime; // Time when the pause button is toggled on
    //private DateTime pauseOffTime; // Time when the pause button is toggled off
    private bool wasRecentlyPaused = false; // Was the pause button recently toggled off?



    private LogManager logManager;  // Create LogManager instance
    private DateTime lastToggleTime; // Added to record last button toggle time
    //private DateTime notificationActivationTime;
    private DateTime goButtonTime;



    //private bool[] notificationShown;



    private void Start()
    {
        goButton.OnClick.AddListener(GoButtonClicked);
        pauseButton.OnClick.AddListener(() => TogglePause(pauseButton.IsToggled));


        logManager = new LogManager();
        logManager.Initialize("SingleToggleParentSetterLog", true); // Initialize LogManager

        if (toggles.Length != stamps.Length)
        {
            Debug.LogError("The count of Toggles does not match the count of Stamps. Please check their counts.");
            return;
        }

        isToggleOn = new bool[toggles.Length];
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].OnClick.AddListener(() => ToggleParent(index));
        }

        backButton.OnClick.AddListener(GoBack);
        backButton.gameObject.SetActive(true); // Hide the back button initially
        toggles[activeToggleIndex].gameObject.SetActive(true); // Show the initial toggle button

        lastToggleTime = DateTime.Now;  // Initialize last toggle time to current time

        //notificationShown = new bool[toggles.Length];
    }


    private void GoButtonClicked()
    {
        // Record the time of the "Go" button click
        goButtonTime = DateTime.Now;
        logManager.WriteToLog("Go button clicked at: " + goButtonTime);
    }

    public void TogglePause(bool isOn)
    {
        if (isOn)
        {
            pauseOnTime = DateTime.Now;
            logManager.WriteToLog("Pause button toggled on");
        }
        else
        {
            pauseOffTime = DateTime.Now;
            wasRecentlyPaused = true;
            logManager.WriteToLog("Pause button toggled off");
        }
    }



    private void FullSceneScreenshot(int index)
    {
        // Create a new camera that will take the screenshot
        GameObject screenshotCamObj = new GameObject("ScreenshotCamera");
        Camera screenshotCam = screenshotCamObj.AddComponent<Camera>();

        // Make sure this camera covers the area you want in your screenshot.
        // You may need to adjust the camera's position, rotation, and field of view to get the desired screenshot.
        // For example:
        screenshotCam.transform.position = new Vector3(0, 0, -0.8f);
        screenshotCam.transform.LookAt(Vector3.zero);

        // Define the screenshot resolution
        int resolutionWidth = 4000; // Modify this based on your needs
        int resolutionHeight = 2500; // Modify this based on your needs

        // Create a new RenderTexture and assign it to the screenshot camera
        RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 24);
        screenshotCam.targetTexture = rt;

        // Render the screenshot camera's view
        screenshotCam.Render();

        // Read the pixels from the RenderTexture
        RenderTexture.active = rt;
        Texture2D screenshotTexture = new Texture2D(resolutionWidth, resolutionHeight, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, resolutionWidth, resolutionHeight), 0, 0);

        // Reset active camera to the main camera
        RenderTexture.active = null;

        // Convert the Texture2D to a PNG and write it to a file
        byte[] bytes = screenshotTexture.EncodeToPNG();
        string dirPath = @"C:\Users\anoth\AppData\LocalLow\DefaultCompany\Debug\user_study\screenshots\ISM Screenshot\Laurie";
        string timestamp = DateTime.Now.ToString("yyMMdd_HHmmss");
        string filename = Path.Combine(dirPath, "Screenshot" + timestamp + "_" + $"step {index + 1}" + ".png");
        File.WriteAllBytes(filename, bytes);

        // Destroy the temporary camera and render texture after capturing the screenshot
        Destroy(rt);
        Destroy(screenshotCamObj);
    }

    public void AddNewObject(GameObject newObj)
    {
        allObjects.Add(newObj);
        logManager.WriteToLog("Added new object: " + newObj.name);
    }

    /*
    public void OnNotificationCloseButtonClicked()
    {
        notificationPrefab.SetActive(false);
        DateTime notificationDeactivationTime = DateTime.Now;
        TimeSpan duration = notificationDeactivationTime - notificationActivationTime;
        logManager.WriteToLog("Notification dimiss reaction time: " + duration.TotalSeconds + " seconds.");
    }
    */

    private void ToggleParent(int index)
    {
        if (activeToggleIndex == index)
        {
            var currentToggleTime = DateTime.Now;

            // If pause button was recently toggled off
            if (wasRecentlyPaused)
            {
                TimeSpan pauseToToggleDuration = currentToggleTime - pauseOffTime;
                logManager.WriteToLog($"Duration between recent Pause button toggle off and step button toggle on: {pauseToToggleDuration.TotalSeconds} seconds");
                wasRecentlyPaused = false;
            }

            var toggleDuration = currentToggleTime - lastToggleTime;  // Duration since last button toggle
            lastToggleTime = currentToggleTime;  // Update last toggle time

            // If this is the first toggle button
            if (index == 0 && !isToggleOn[index])
            {
                // Calculate and log the duration between the "Go" button click and the first toggle button being toggled on
                var goButtonDuration = currentToggleTime - goButtonTime;
                logManager.WriteToLog("Duration between Go button click and first toggle button toggle: " + goButtonDuration.TotalSeconds + " seconds");
            }



            isToggleOn[index] = !isToggleOn[index];
            logManager.WriteToLog($"Toggled button {index + 1}. State: {(isToggleOn[index] ? "On" : "Off")}. Duration since last toggle: {toggleDuration.TotalSeconds} seconds");

            // Start the coroutine with the operations after screenshot
            StartCoroutine(PerformOperationsAfterScreenshot(index));

            // Set corresponding stamp active/inactive
            stamps[index].SetActive(isToggleOn[index]);

            /*
            if (index == 0 || index == 5 || index == 10)
            {
                if (!notificationShown[index]) // Check if notification hasn't been shown for this step
                {
                    // If yes, then start the notification activation after a delay
                    StartCoroutine(ActivateNotificationAfterDelay(8, 18));
                    notificationShown[index] = true; // Mark this step's notification as shown
                }
            }
            */
        }
    }


    private IEnumerator PerformOperationsAfterScreenshot(int index)
    {
        // Define your directory path
        string dirPath = @"C:\Users\anoth\AppData\LocalLow\DefaultCompany\Debug\user_study\screenshots\ISM Screenshot\Laurie";

        // Check if the directory exists, if not, create it
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        // Wait for the end of frame so the screenshot captures the final rendered image
        yield return new WaitForEndOfFrame();

        // Take a screenshot and save to defined directory
        FullSceneScreenshot(index);

        yield return new WaitForSeconds(0.5f);

        if (isToggleOn[index])
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj.transform.parent == null)
                {
                    obj.transform.SetParent(stepParents[index].transform);
                    logManager.WriteToLog("Set parent for object: " + obj.name + ". New parent: " + obj.transform.parent.name);
                }
            }

            // Set parent to invisible when toggled on
            stepParents[index].SetActive(false);

            // Hide the current toggle button
            toggles[activeToggleIndex].gameObject.SetActive(false);

            activeToggleIndex++;

            if (activeToggleIndex < toggles.Length)
            {
                // Show the next toggle button as untoggled
                toggles[activeToggleIndex].IsToggled = false;
                toggles[activeToggleIndex].gameObject.SetActive(true);

                // Set the next step parent visible and unparent its children
                stepParents[activeToggleIndex].SetActive(true);
                foreach (Transform child in stepParents[activeToggleIndex].transform)
                {
                    child.SetParent(null);
                    logManager.WriteToLog("Released child object: " + child.name);
                }
            }
        }
        else
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj.transform.parent == stepParents[index].transform) // Only unparent if this is its parent
                {
                    obj.transform.SetParent(null);
                    logManager.WriteToLog("Unparented object: " + obj.name);
                }
            }

            // Set parent to visible when toggled off
            stepParents[index].SetActive(true);
        }
    }

    /*
    private IEnumerator ActivateNotificationAfterDelay(int minDelay, int maxDelay)
    {
        int delay = UnityEngine.Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        notificationPrefab.SetActive(true);
        notificationActivationTime = DateTime.Now;  // Record the time of activation
        logManager.WriteToLog("Notification prefab activated after a delay of " + delay + " seconds.");
    }
    */


    private void GoBack()
    {
        if (activeToggleIndex > 0)
        {
            // Toggle off the current active toggle button
            isToggleOn[activeToggleIndex] = false;
            toggles[activeToggleIndex].gameObject.SetActive(false); // Hide the current toggle button
            stamps[activeToggleIndex].SetActive(false); // Set the corresponding stamp to inactive

            // Set current step parent invisible
            stepParents[activeToggleIndex].SetActive(false);

            // Reparent existing objects to the current step parent
            foreach (GameObject obj in allObjects)
            {
                if (obj.transform.parent == null) // Only set parent if it doesn't already have one
                {
                    obj.transform.SetParent(stepParents[activeToggleIndex].transform);
                    logManager.WriteToLog("Set parent for object: " + obj.name + ". New parent: " + obj.transform.parent.name);
                }
            }

            activeToggleIndex--;

            // Toggle off the previous toggle button
            isToggleOn[activeToggleIndex] = false;
            stamps[activeToggleIndex].SetActive(isToggleOn[activeToggleIndex]); // Set the corresponding stamp to the same state as the toggle

            // Set previous button as untoggled and visible
            toggles[activeToggleIndex].gameObject.SetActive(true);

            // Set previous step parent visible and unparent it
            stepParents[activeToggleIndex].SetActive(true);

            foreach (Transform child in stepParents[activeToggleIndex].transform)
            {
                child.SetParent(null);
                logManager.WriteToLog("Unparented child object: " + child.name);
            }

            logManager.WriteToLog("Went back to button " + (activeToggleIndex + 1));

            isBackButtonClicked = true;
        }

        if (activeToggleIndex == 0)
        {
            backButton.gameObject.SetActive(true); // Hide the back button if on the first toggle button
        }
    }


    private void Update()
    {
        CheckForKeyboardInput();
    }

    private void CheckForKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ToggleParent(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ToggleParent(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ToggleParent(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ToggleParent(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ToggleParent(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) ToggleParent(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) ToggleParent(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) ToggleParent(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) ToggleParent(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) ToggleParent(9);
        if (Input.GetKeyDown(KeyCode.Q)) ToggleParent(10);
        if (Input.GetKeyDown(KeyCode.W)) ToggleParent(11);
        if (Input.GetKeyDown(KeyCode.E)) ToggleParent(12);
        if (Input.GetKeyDown(KeyCode.R)) ToggleParent(13);
        if (Input.GetKeyDown(KeyCode.T)) ToggleParent(14);

        // For the back button
        if (Input.GetKeyDown(KeyCode.B)) GoBack();
    }

}