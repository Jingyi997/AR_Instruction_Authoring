using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        // Assuming you are using the main camera as the HoloLens camera.
        // Otherwise, set this to the appropriate camera transform.
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Make the button face the camera.
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
            mainCameraTransform.rotation * Vector3.up);
    }
}
