// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    [AddComponentMenu("Scripts/MRTK/Examples/SliderUniformScaler")]
    public class SliderUniformScaler : MonoBehaviour
    {
        [SerializeField]
        private Transform scalableObject = null; // GameObject to be scaled

        private Vector3 initialScale; // Initial scale of the GameObject

        private float minScale = 0.8f; // Minimum scale factor
        private float maxScale = 1.8f; // Maximum scale factor

        private void Start()
        {
            if (scalableObject == null)
            {
                Debug.LogError("ScalableObject is missing. Please attach it.");
                return;
            }
            initialScale = scalableObject.localScale;
        }

        public void OnSliderUpdated(SliderEventData eventData)
        {
            if (scalableObject != null)
            {
                // Map the slider value from [0,1] to [minScale, maxScale]
                float scale = Mathf.Lerp(minScale, maxScale, eventData.NewValue);

                // Scale the GameObject using the mapped scale value
                scalableObject.localScale = initialScale * scale;
            }
        }
    }
}
