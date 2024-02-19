using System.Collections;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    // Set these as the tooltip GameObjects in the inspector
    public GameObject[] tooltips; // Store all tooltips in an array
    private bool firstClickDone = false;

    private void Update()
    {
        // Check if 'P' key is pressed and it's the first time
        if (Input.GetKeyUp(KeyCode.S) && !firstClickDone)
        {
            // Start the coroutine to display tooltips in sequence
            StartCoroutine(ActivateAndDestroyTooltipsInSequence());

            firstClickDone = true;
        }
    }

    IEnumerator ActivateAndDestroyTooltipsInSequence()
    {
        // Loop through each tooltip in the array
        foreach (var tooltip in tooltips)
        {
            if (tooltip != null)
            {
                // Activate the tooltip
                tooltip.SetActive(true);

                // Wait for 5 seconds
                yield return new WaitForSeconds(8);

                // Destroy the tooltip
                Destroy(tooltip);
            }
        }
    }
}
