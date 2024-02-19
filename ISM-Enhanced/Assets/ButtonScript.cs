using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject parentObject;  // Assign the parent object in the Unity inspector

    public void OnButtonPress()
    {
        // Do whatever the button is supposed to do

        // Notify the parent object that it should not move
        parentObject.GetComponent<ParentObjectScript>().DisableMovementTemporarily();
    }

    public void OnButtonReleased()
    {
        // Notify the parent object that it can move now
        parentObject.GetComponent<ParentObjectScript>().EnableMovement();
    }
}
