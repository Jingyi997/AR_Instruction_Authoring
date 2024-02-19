using UnityEngine;
using UnityEngine.UI;

public class DrawLineButton : MonoBehaviour
{
    public GameObject linePrefab;
    private LineRenderer currentLineRenderer;

    private bool drawingMode = false; // To control the drawing mode

    public void OnButtonClicked() // This function should be linked to button onClick event
    {
        drawingMode = true; // Enable the drawing mode
    }

    void Update()
    {
        // If drawingMode is true and there's a touch input
        if (drawingMode && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Instantiate the line renderer
                GameObject lineObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                currentLineRenderer = lineObject.GetComponent<LineRenderer>();
                currentLineRenderer.positionCount = 2;

                // Set the starting position as the button's position
                Vector3 buttonPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                currentLineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(buttonPos));

                // Update the ending position with the touch position
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                currentLineRenderer.SetPosition(1, new Vector3(touchPos.x, touchPos.y, 0));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Update the ending position with the new touch position
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                currentLineRenderer.SetPosition(1, new Vector3(touchPos.x, touchPos.y, 0));
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // When touch ends, disable the drawing mode
                drawingMode = false;
            }
        }
    }
}
