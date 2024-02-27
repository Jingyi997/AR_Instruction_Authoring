using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Utilities
{
    /// <summary>
    /// Set the content around the camera height
    /// </summary>
    
    public class SampleHeadOffset : MonoBehaviour
    {
        public Vector3 HeadOffset = new Vector3(0.423f, 0.117f, 0.741f);

        private bool started = false;

        private void Start()
        {
            transform.position = HeadOffset;
            started = true;
        }

        private void OnEnable()
        {
            if (started)
            {
                transform.position = HeadOffset;
            }
        }
    }
}
