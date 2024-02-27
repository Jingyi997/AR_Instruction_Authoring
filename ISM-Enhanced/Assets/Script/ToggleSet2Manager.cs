using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ToggleSet2Manager : MonoBehaviour
{
    private int currentStep;
    private GameObject goBackButton;
    private Interactable[] toggleButtons;

    private void Start()
    {
        GameObject parentObject = GameObject.Find("Toggle Set 2");
        toggleButtons = new Interactable[15];
        goBackButton = GameObject.Find("Go Back");

        for (int i = 0; i < 15; i++)
        {
            toggleButtons[i] = parentObject.transform.Find("Button " + (i + 1)).GetComponent<Interactable>();
            toggleButtons[i].gameObject.SetActive(false);  // Set all buttons inactive at the start
        }

        currentStep = 1;
        toggleButtons[0].gameObject.SetActive(true);  // Set button 1 active at the start
    }

    public void GoBackAction(int step)
    {
        if (currentStep > 1)
        {
            toggleButtons[currentStep - 1].gameObject.SetActive(false);
            toggleButtons[currentStep - 2].gameObject.SetActive(true);
            toggleButtons[currentStep - 2].IsToggled = false;
            goBackButton.SetActive(currentStep - 2 > 0);
            currentStep--;
        }
    }

    public void ToggleButton(int step)
    {
        if (step < 1 || step > 15)
        {
            Debug.LogError("Invalid step: " + step);
            return;
        }

        toggleButtons[step - 1].gameObject.SetActive(false);
        if (step < 15)
        {
            toggleButtons[step].gameObject.SetActive(true);
        }
        goBackButton.SetActive(step > 0);
        currentStep = step + 1;
    }
}
