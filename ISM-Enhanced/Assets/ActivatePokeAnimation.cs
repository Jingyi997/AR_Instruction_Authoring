using UnityEngine;

public class ActivatePokeAnimation : MonoBehaviour
{
    public GameObject objectToActivate;  // Drag your GameObject here in the inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            objectToActivate.SetActive(true);
        }
    }
}
