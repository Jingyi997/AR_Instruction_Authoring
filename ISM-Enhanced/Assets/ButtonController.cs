using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject linkedObject; // This will be your parent object

    public void ToggleVisibility(bool state)
    {
        linkedObject.SetActive(state);
    }
}
