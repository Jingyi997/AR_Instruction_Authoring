using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class DwellButton : MonoBehaviour
{
    public float dwellTime = 2f; // Time in seconds
    private float currentTime = 0f;
    private bool isPressed = false;

    public Transform dwellVisualImage; // Assign this from the inspector
    public float cancelStartScale = 1f;
    public float dwellVisualCancelDurationInFrames = 60f;

    private void OnEnable()
    {
        GetComponent<PressableButton>().ButtonPressed.AddListener(OnButtonPressed);
        GetComponent<PressableButton>().ButtonReleased.AddListener(OnButtonReleased);
    }

    private void OnDisable()
    {
        GetComponent<PressableButton>().ButtonPressed.RemoveListener(OnButtonPressed);
        GetComponent<PressableButton>().ButtonReleased.RemoveListener(OnButtonReleased);
    }

    private void OnButtonPressed()
    {
        isPressed = true;
        currentTime = 0f;
    }

    private void OnButtonReleased()
    {
        isPressed = false;
    }

    private void Update()
    {
        if (isPressed)
        {
            currentTime += Time.deltaTime;
            float value = currentTime / dwellTime; // Calculate the progress
            dwellVisualImage.localScale = new Vector3(value, value, value); // Scale the visual

            if (currentTime >= dwellTime)
            {
                DwellAction();
                isPressed = false;
                currentTime = 0f;
            }
        }
        else if (!isPressed && dwellVisualImage.localScale.x > 0)
        {
            float value = Mathf.Clamp(dwellVisualImage.localScale.x - (cancelStartScale / dwellVisualCancelDurationInFrames), 0f, 1f);
            dwellVisualImage.localScale = new Vector3(value, value, value);
        }
    }

    private void DwellAction()
    {
        Debug.Log("Dwell action performed");
        // Here is where you'd put the functionality for the dwell action
    }
}
