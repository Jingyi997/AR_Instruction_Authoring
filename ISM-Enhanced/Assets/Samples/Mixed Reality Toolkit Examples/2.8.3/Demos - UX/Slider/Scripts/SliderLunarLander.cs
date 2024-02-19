// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Video; // Added this to use VideoPlayer

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    [AddComponentMenu("Scripts/MRTK/Examples/SliderLunarLander")]
    public class SliderLunarLander : MonoBehaviour
    {
        [SerializeField]
        private Transform transformLandingGear = null;

        [SerializeField] // Add this line
        private VideoPlayer videoPlayer = null; // Add this line

        private void Start()
        {
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component is missing. Please attach it.");
                return;
            }
            // ensure that 'playOnAwake' is false, so we have control over when it starts
            videoPlayer.playOnAwake = false;
            // prepare the video to play
            videoPlayer.Prepare();
        }

        public void OnSliderUpdated(SliderEventData eventData)
        {
            if (transformLandingGear != null)
            {
                // Rotate the target object using Slider's eventData.NewValue
                transformLandingGear.localPosition = new Vector3(transformLandingGear.localPosition.x, 1.0f - eventData.NewValue, transformLandingGear.localPosition.z);
            }

            if (videoPlayer != null && videoPlayer.isPrepared) // Added this
            {
                // Map the slider value (0-1) to video player frame
                long frame = (long)(eventData.NewValue * videoPlayer.frameCount);
                videoPlayer.frame = frame;
            }
        }
    }
}
