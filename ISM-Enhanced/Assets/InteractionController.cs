using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System;

public class InteractionController : MonoBehaviour
{
    private IMixedRealityInputSystem inputSystem;

    private void OnEnable()
    {
        inputSystem = GetComponent<IMixedRealityInputSystem>();
    }

    private void Update()
    {
        if (inputSystem != null)
        {
            foreach (IMixedRealityPointer pointer in inputSystem.FocusProvider.GetPointers<ShellHandRayPointer>())
            {
                IMixedRealityNearPointer nearPointer = pointer as IMixedRealityNearPointer;
                if (nearPointer != null && nearPointer.IsNearObject)
                {
                    // If near interaction is happening, disable the hand ray
                    pointer.BaseCursor.SetVisibility(false);
                }
                else
                {
                    // Otherwise, enable it
                    pointer.BaseCursor.SetVisibility(true);
                }
            }
        }
    }
}
