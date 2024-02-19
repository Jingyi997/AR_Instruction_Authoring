using UnityEngine;
using UnityEngine.UI;

public class ManualScroller : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public RectTransform contentPanel;
    public float scrollDistance = 200f;

    void Start()
    {
        leftButton.onClick.AddListener(() => ScrollContent(-scrollDistance));
        rightButton.onClick.AddListener(() => ScrollContent(scrollDistance));
    }

    public void ScrollContent(float distance)
    {
        Vector3 newPosition = contentPanel.localPosition;
        newPosition.x += distance;
        contentPanel.localPosition = newPosition;
    }
}
