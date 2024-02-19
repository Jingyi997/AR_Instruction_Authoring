using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class PinContent : MonoBehaviour
{
    public Interactable PinButton; // Assign this in the Unity editor
    public Transform Camera; // Assign this to your camera (HoloLens device)
    public float DistanceFromCamera = 2f; // How far in front of the camera the content should appear

    private bool isPinned = false;

    private void Start()
    {
        // Attach the pin button press event to our Pin method
        PinButton.OnClick.AddListener(Pin);
    }

    private void Update()
    {
        // If the content is not pinned, update its position and rotation to follow the head
        if (!isPinned)
        {
            Vector3 targetPosition = Camera.position + Camera.forward * DistanceFromCamera;
            transform.position = targetPosition;
            transform.rotation = Camera.rotation;
        }
    }

    private void Pin()
    {
        // Toggle the pin state
        isPinned = !isPinned;
    }
}

