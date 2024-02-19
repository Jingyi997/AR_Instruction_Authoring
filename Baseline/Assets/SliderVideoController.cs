using System.Collections; // Added this to use IEnumerator
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Video; // Added this to use VideoPlayer


namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    [AddComponentMenu("Scripts/MRTK/Examples/SliderVideoController")]
    public class SliderVideoController : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer videoPlayer = null;
        [SerializeField]
        private PinchSlider videoSlider = null;  // Add this line to reference the slider
        [SerializeField]
        private Interactable playStopButton = null;


        private bool sliderIsMoving = false; // Add this line to track whether the slider is being manually moved

        private void Start()
        {
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component is missing. Please attach it.");
                return;
            }
            if (videoSlider == null) // Check for the slider as well
            {
                Debug.LogError("PinchSlider component is missing. Please attach it.");
                return;
            }
            // ensure that 'playOnAwake' is false, so we have control over when it starts
            videoPlayer.playOnAwake = false;
            // prepare the video to play
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
            videoSlider.OnInteractionStarted.AddListener((data) => { sliderIsMoving = true; });
            videoSlider.OnInteractionEnded.AddListener((data) => { sliderIsMoving = false; });
            videoSlider.OnValueUpdated.AddListener(OnSliderUpdated);

            if (playStopButton == null)
            {
                Debug.LogError("PlayStopButton component is missing. Please attach it.");
                return;
            }

            playStopButton.OnClick.AddListener(ToggleVideoPlayback);

        }

        private void VideoPlayer_prepareCompleted(VideoPlayer source)
        {
            // As soon as the video is prepared, start playing it
            videoPlayer.Play();
            StartCoroutine(UpdateSliderAsVideoPlays());
        }

        private IEnumerator UpdateSliderAsVideoPlays()
        {
            while (true)
            {
                if (!sliderIsMoving && videoPlayer.isPrepared)
                {
                    float sliderValue = (float)videoPlayer.frame / videoPlayer.frameCount;
                    videoSlider.SliderValue = sliderValue;
                }
                yield return null;
            }
        }

        public void OnSliderUpdated(SliderEventData eventData)
        {
            if (videoPlayer != null && videoPlayer.isPrepared)
            {
                // Map the slider value (0-1) to video player frame
                long frame = (long)(eventData.NewValue * videoPlayer.frameCount);
                videoPlayer.frame = frame;
            }
        }

        public void ToggleVideoPlayback()
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else if (videoPlayer.isPrepared)
            {
                videoPlayer.Play();
            }
        }

    }
}
