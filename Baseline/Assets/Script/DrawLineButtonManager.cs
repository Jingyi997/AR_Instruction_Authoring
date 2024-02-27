using UnityEngine;

public class DrawLineButtonManager : MonoBehaviour
{
    public GameObject drawLineButton;

    private void Start()
    {
        drawLineButton.SetActive(false);
    }

    public void OnHoverEntered()
    {
        drawLineButton.SetActive(true);
    }

    public void OnHoverExited()
    {
        drawLineButton.SetActive(false);
    }
}