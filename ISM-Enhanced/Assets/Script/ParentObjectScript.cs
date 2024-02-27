using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ParentObjectScript : MonoBehaviour
{
    public ObjectManipulator objectManipulator;

    public void DisableMovementTemporarily()
    {
        objectManipulator.enabled = false;
    }

    public void EnableMovement()
    {
        objectManipulator.enabled = true;
    }
}
