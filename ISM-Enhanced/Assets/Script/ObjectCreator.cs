using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject sampleObject;
    public ObjectCreationManager creationManager;

    public void OnButtonClick()
    {
        Vector3 specificPosition = new Vector3(0.06055248f, -0.002603434f, 0.3900869f);
        GameObject newObj = Instantiate(sampleObject, specificPosition, Quaternion.identity);
        creationManager.AddObjectToList(newObj);
    }
}
