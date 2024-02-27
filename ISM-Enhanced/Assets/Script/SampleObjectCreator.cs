using UnityEngine;

public class SampleObjectCreator : MonoBehaviour
{
    public GameObject sampleObject;
    public ObjectParentSetter parentSetter;  // Reference to the ObjectParentSetter script

    public void CreateObject()
    {
        // Instantiate new object and add it to the list in the ObjectParentSetter

        Vector3 specificPosition = new Vector3(-0.1436258f, -0.1099623f, 0.2372633f);

        GameObject newObj = Instantiate(sampleObject, specificPosition, Quaternion.identity);
        parentSetter.AddNewObject(newObj);
    }
}