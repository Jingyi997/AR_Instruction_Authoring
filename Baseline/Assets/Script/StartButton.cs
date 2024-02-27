using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject toggleSet2;
    public GameObject backButton;
    public GameObject startButton;

    // This method can be called from MRTK button OnClick event
    public void OnStartButtonClick()
    {
        // Set active the "Toggle Set 2" gameobject
        if (toggleSet2 != null)
        {
            toggleSet2.SetActive(true);
        }
        else
        {
            Debug.LogError("Toggle Set 2 GameObject is not assigned in the inspector");
        }

        // Set active the "Back" gameobject
        if (backButton != null)
        {
            backButton.SetActive(true);
        }
        else
        {
            Debug.LogError("Back Button GameObject is not assigned in the inspector");
        }

        // Destroy the "start" button
        if (startButton != null)
        {
            Destroy(startButton);
        }
        else
        {
            Debug.LogError("Start Button GameObject is not assigned in the inspector");
        }
    }
}
