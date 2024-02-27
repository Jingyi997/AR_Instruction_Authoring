using UnityEngine;

public class ActivateOnKeyPress : MonoBehaviour
{
    public GameObject objectToActivate;  // Drag your GameObject here in the inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            objectToActivate.SetActive(true);
        }
    }
}
