using System.Collections;
using UnityEngine;

public class EditButton : MonoBehaviour
{
    public GameObject EditButtonObject;
    public GameObject HideButtonObject;
    public GameObject DrawLineButtonObject;
    public GameObject RemoveButtonObject;

    private void Start()
    {
        // Initially, only 'Edit' is active
        SetEditActive();
    }

    // This function is to be linked with the 'Edit' button's onClick event
    public void OnEditButtonPressed()
    {
        StartCoroutine(ActivateOtherButtons());
    }

    // This function is to be linked with the 'Hide' button's onClick event
    public void OnHideButtonPressed()
    {
        StartCoroutine(DeactivateOtherButtons());
    }

    private IEnumerator ActivateOtherButtons()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Set 'Edit' inactive
        EditButtonObject.SetActive(false);

        // Set other buttons active
        HideButtonObject.SetActive(true);
        DrawLineButtonObject.SetActive(true);
        RemoveButtonObject.SetActive(true);
    }

    private IEnumerator DeactivateOtherButtons()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second

        // Set 'Hide', 'Draw Line Button', and 'Remove' inactive
        HideButtonObject.SetActive(false);
        DrawLineButtonObject.SetActive(false);
        RemoveButtonObject.SetActive(false);

        // Set 'Edit' active again
        EditButtonObject.SetActive(true);
    }

    private void SetEditActive()
    {
        EditButtonObject.SetActive(true);
        HideButtonObject.SetActive(false);
        DrawLineButtonObject.SetActive(false);
        RemoveButtonObject.SetActive(false);
    }
}
