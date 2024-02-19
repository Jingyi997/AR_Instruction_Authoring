using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ButtonScaler : MonoBehaviour
{
    public GameObject layer;
    public Vector3 targetScale = new Vector3(0.32f, 0.32f, 0.32f);
    public Vector3 initialScale = Vector3.zero;

    void Start()
    {
        layer.transform.localScale = initialScale;
    }

    public void OnButtonPressed()
    {
        layer.transform.localScale = targetScale;
    }

    public void OnButtonReleased()
    {
        layer.transform.localScale = initialScale;
    }
}
