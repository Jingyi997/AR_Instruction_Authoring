using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class RemoveLineButtonController : MonoBehaviour
{
    public GameObject lineObject; // Reference to the associated line object
    public Interactable firstButtonInteractable; // Reference to the first button's Interactable component
    public Interactable secondButtonInteractable; // Reference to the second button's Interactable component

    public void RemoveLine()
    {
        if (lineObject != null)
        {
            Destroy(lineObject);
        }

        // Untoggle the paired buttons
        if (firstButtonInteractable != null)
        {
            firstButtonInteractable.IsToggled = false;
        }
        if (secondButtonInteractable != null)
        {
            secondButtonInteractable.IsToggled = false;
        }

        // Destroy the remove button itself
        Destroy(gameObject);
    }
}
