using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class SelectableButton : MonoBehaviour
{
    private Interactable interactableComponent;

    private void Start()
    {
        interactableComponent = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (interactableComponent.IsToggled)
        {
            LineManager.Instance.SelectButton(gameObject);
        }
        else
        {
            LineManager.Instance.RemoveLine();
        }
    }
}