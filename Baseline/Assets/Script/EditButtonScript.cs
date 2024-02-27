using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is necessary to interact with UI components.

public class EditButtonScript : MonoBehaviour
{
    // Drag the Button GameObjects for "draw line" and "remove" here in the inspector.
    public GameObject drawLineButton;
    public GameObject removeButton;

    // Directly reference the Toggle component from the inspector.
    public Toggle editToggle;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure editToggle isn't null before adding the listener
        if (editToggle != null)
        {
            editToggle.onValueChanged.AddListener(delegate {
                ToggleValueChanged(editToggle);
            });
        }
        else
        {
            Debug.LogError("editToggle is null. Please assign the Toggle in the inspector.");
        }
    }

    // This method gets called whenever the toggle value changes.
    void ToggleValueChanged(Toggle change)
    {
        bool isActive = change.isOn;

        // Set "draw line" and "remove" buttons active or inactive depending on the toggle state.
        drawLineButton.SetActive(isActive);
        removeButton.SetActive(isActive);
    }
}
