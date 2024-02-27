using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    private float targetScrollPos = 0f;
    private float totalColumns = 7f; // total columns
    private float viewableColumns = 4f; // visible columns
    private float stepSize;

    private void Start()
    {
        stepSize = 1f / ((totalColumns - viewableColumns) / 2f);
    }

    public void ScrollLeft()
    {
        targetScrollPos = Mathf.Clamp(targetScrollPos - stepSize, 0, 1);
    }

    public void ScrollRight()
    {
        targetScrollPos = Mathf.Clamp(targetScrollPos + stepSize, 0, 1);
    }

    private void Update()
    {
        scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetScrollPos, 0.1f);
    }
}
