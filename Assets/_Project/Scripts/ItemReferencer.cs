using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReferencer : MonoBehaviour
{
    public static ItemReferencer Instance = null;

    [Header("Items To Reference")]
    public InteractableTooltip interactableToolTipPrefab;
    public Transform interactableToolTipCanvasTransform;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
