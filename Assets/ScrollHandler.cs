using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollTime = 0.5f;
    private bool isScrolling;

    // Scroll to the start
    public void ScrollLeft()
    {
        if (!isScrolling)
        {
            StartCoroutine(ScrollTo(0));
        }
    }

    // Scroll to the end
    public void ScrollRight()
    {
        if (!isScrolling)
        {
            StartCoroutine(ScrollTo(1));
        }
    }

    IEnumerator ScrollTo(float target)
    {
        isScrolling = true;

        float elapsedTime = 0;
        float startPosition = scrollRect.horizontalNormalizedPosition;

        while (elapsedTime < scrollTime)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, target, elapsedTime / scrollTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure the scrollrect ends up at the target position
        scrollRect.horizontalNormalizedPosition = target;

        isScrolling = false;
    }
}
