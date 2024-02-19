using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    public GameObject removeButton;
    private RemoveButtonManager removeButtonManager;

    private void Start()
    {
        removeButton.SetActive(true);
        removeButtonManager = FindObjectOfType<RemoveButtonManager>(); // Finds the RemoveButtonManager in the scene
        removeButtonManager.RegisterButton(removeButton);
    }

    /*
    public void OnHoverEntered()
    {
        removeButton.SetActive(true);
    }

    public void OnHoverExited()
    {
        removeButton.SetActive(false);
    }
    */

    // Call this method when the button is clicked
    public void OnButtonClick()
    {
        // Log the button click
        removeButtonManager.LogButtonClick(removeButton.name);
    }
}
