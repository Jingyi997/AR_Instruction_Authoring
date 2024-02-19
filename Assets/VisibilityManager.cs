using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI; // Import MRTK

public class VisibilityManager : MonoBehaviour
{
    // Your array of Toggle Buttons
    private Interactable[] toggleVisibilityButtons;

    // Your array of stepParents already in your script
    private GameObject[] stepParents;

    private void Start()
    {
        toggleVisibilityButtons = new Interactable[15];
        stepParents = new GameObject[15];
        for (int i = 1; i <= 15; i++)
        {
            // Find each button and step parent in the scene
            toggleVisibilityButtons[i - 1] = GameObject.Find("ToggleVisibilityButton " + i).GetComponent<Interactable>();
            stepParents[i - 1] = GameObject.Find("Step " + i);

            // Set step parents invisible by default
            stepParents[i - 1].SetActive(false);

            // Add an event handler to the button
            int step = i;
            toggleVisibilityButtons[i - 1].OnClick.AddListener(() => ToggleVisibility(step));
        }
    }

    public void ToggleVisibility(int step)
    {
        // Toggle visibility of the corresponding step parent
        stepParents[step - 1].SetActive(!stepParents[step - 1].activeSelf);
    }
}
