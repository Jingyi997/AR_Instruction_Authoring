using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    private Vector3[] initialScales;
    private Transform[] children;
    private bool isManipulating;

    private void Awake()
    {
        children = new Transform[transform.childCount];
        initialScales = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            initialScales[i] = children[i].localScale;

            var manipulator = children[i].GetComponent<ObjectManipulator>();
            if (manipulator != null)
            {
                manipulator.OnManipulationStarted.AddListener(StartManipulation);
                manipulator.OnManipulationEnded.AddListener(StopManipulation);
            }
        }
    }

    private void StartManipulation(ManipulationEventData eventData)
    {
        isManipulating = true;
    }

    private void StopManipulation(ManipulationEventData eventData)
    {
        isManipulating = false;
        ScaleAllChildren();
    }

    private void Update()
    {
        if (isManipulating)
        {
            ScaleAllChildren();
        }
    }

    private void ScaleAllChildren()
    {
        float scaleFactor = children[0].localScale.magnitude / initialScales[0].magnitude;

        for (int i = 1; i < children.Length; i++)
        {
            children[i].localScale = initialScales[i] * scaleFactor;
        }
        Debug.Log("Test to see if its called!!!!!!!");

    }
}
