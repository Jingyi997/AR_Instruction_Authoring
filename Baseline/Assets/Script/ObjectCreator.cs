using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject sampleObject;
    public ObjectCreationManager creationManager;

    public void OnButtonClick()
    {
        Vector3 specificPosition = new Vector3(0.1004482f, 0.09202963f, 0.752513f);
        GameObject newObj = Instantiate(sampleObject, specificPosition, Quaternion.identity);
        creationManager.AddObjectToList(newObj);
    }
}
