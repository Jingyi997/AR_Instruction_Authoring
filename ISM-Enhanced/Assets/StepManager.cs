using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class StepManager : MonoBehaviour
{
    public GameObject[] buttons;
    private int activeButtonIndex = 0;

    void Start()
    {
        buttons[activeButtonIndex].SetActive(true);
    }

    public void OnButtonClick()
    {
        buttons[activeButtonIndex].SetActive(false); // Deactivate the current button

        activeButtonIndex++;

        if (activeButtonIndex < buttons.Length)
        {
            buttons[activeButtonIndex].SetActive(true); // Activate the next button
        }
    }
}
