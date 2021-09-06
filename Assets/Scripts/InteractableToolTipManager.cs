using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableToolTipManager : MonoBehaviour
{
    [SerializeField] private InteractableTooltip interactableTooltip;
    public static Action<GameObject, string> onInteractableInRange;
    public static Action onInteractableOutOfRange;

    private void OnEnable()
    {
        onInteractableInRange += ShowToolTip;
        onInteractableOutOfRange += HideToolTip;
    }

    private void OnDisable()
    {
        onInteractableInRange -= ShowToolTip;
        onInteractableOutOfRange -= HideToolTip;
    }

    private void ShowToolTip(GameObject interactable, string keyToPress)
    {
        interactableTooltip.transform.position = Camera.main.WorldToScreenPoint(interactable.transform.position + new Vector3(0, .5f, 0));
        interactableTooltip.SetToolTipText("Press " + keyToPress);
        interactableTooltip.gameObject.SetActive(true);
    }

    private void HideToolTip()
    {
        interactableTooltip.gameObject.SetActive(false);
        interactableTooltip.SetToolTipText("");
    }
}
