using UnityEngine;

public class SingleToggleSampleObjectCreator : MonoBehaviour
{
    public GameObject sampleObject;
    public SingleToggleParentSetter parentSetter;
    public Camera mainCamera;  // Add a reference to your main camera.

    public void CreateObject()
    {
        // Define the relative offset.
        Vector3 relativeOffset = new Vector3(0f, 0f, 0.6f);

        // Compute the actual position.
        Vector3 specificPosition = mainCamera.transform.position + mainCamera.transform.rotation * relativeOffset;

        // Instantiate new object and add it to the list in the ObjectParentSetter
        GameObject newObj = Instantiate(sampleObject, specificPosition, Quaternion.identity);
        parentSetter.AddNewObject(newObj);
    }
}
