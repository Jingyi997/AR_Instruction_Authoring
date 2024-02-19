using UnityEngine;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance; // Singleton instance
    public GameObject linePrefab; // The line prefab with a LineRenderer component

    private List<GameObject> selectedButtons = new List<GameObject>();
    private LineRenderer currentLineRenderer; // Store this as an instance variable

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectButton(GameObject button)
    {
        GameObject buttonParent = button.transform.parent.gameObject;

        // If this button has already been selected, ignore the selection
        if (selectedButtons.Contains(buttonParent))
        {
            return;
        }

        selectedButtons.Add(buttonParent);

        if (selectedButtons.Count >= 2)
        {
            // If a line is already being rendered, remove it before drawing a new one
            if (currentLineRenderer != null)
            {
                Destroy(currentLineRenderer.gameObject);
            }

            GameObject firstButton = selectedButtons[selectedButtons.Count - 2];
            GameObject secondButton = selectedButtons[selectedButtons.Count - 1];

            // Instantiate the line renderer
            GameObject lineObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            currentLineRenderer = lineObject.GetComponent<LineRenderer>();
            currentLineRenderer.positionCount = 2;

            // Set the positions
            currentLineRenderer.SetPosition(0, firstButton.transform.position);
            currentLineRenderer.SetPosition(1, secondButton.transform.position);
        }
    }


    public void RemoveLine()
    {
        if (currentLineRenderer != null)
        {
            Destroy(currentLineRenderer.gameObject);
            currentLineRenderer = null;
            selectedButtons.Clear();
        }
    }

    void Update()
    {
        // If a line is being rendered, update its points
        if (currentLineRenderer != null && selectedButtons.Count >= 2)
        {
            GameObject firstButton = selectedButtons[selectedButtons.Count - 2];
            GameObject secondButton = selectedButtons[selectedButtons.Count - 1];

            currentLineRenderer.SetPosition(0, firstButton.transform.position);
            currentLineRenderer.SetPosition(1, secondButton.transform.position);

            // Check if either button or their parent is inactive
            if (!firstButton.activeInHierarchy || !secondButton.activeInHierarchy)
            {
                // If one of the buttons or their parent is inactive, hide the line
                currentLineRenderer.enabled = false;
            }
            else
            {
                // If both buttons and their parents are active, show the line
                currentLineRenderer.enabled = true;
            }
        }
    }
}