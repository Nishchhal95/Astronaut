using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool showInteractableToolTip = false;
    [SerializeField] protected InteractableTooltip interactableTooltip;
    [SerializeField] protected Vector3 interactableToolTipOffset = new Vector3(0, .2f, 0);
    public abstract void Interact();
    public virtual void OnPlayerEnterInteractionZone()
    {
        if(interactableTooltip == null)
        {
            interactableTooltip = SpawnInteractableToolTip();
        }
        interactableTooltip.gameObject.SetActive(false);
        SetupInteractableToolTip(interactableTooltip);
    }
    public virtual void OnPlayerExitInteractionZone()
    {
        //interactableTooltip.gameObject.LeanScale(Vector3.zero, .2f).setOnComplete(() =>
        //{
        //    interactableTooltip.gameObject.SetActive(false);
        //    interactableTooltip.SetToolTipText("");
        //});
        interactableTooltip.gameObject.SetActive(false);
        interactableTooltip.SetToolTipText("");
        showInteractableToolTip = false;
    }

    private InteractableTooltip SpawnInteractableToolTip()
    {
        return Instantiate(ItemReferencer.Instance.interactableToolTipPrefab, ItemReferencer.Instance.interactableToolTipCanvasTransform);
    }

    private void SetupInteractableToolTip(InteractableTooltip interactableTooltip)
    {
        interactableTooltip.SetToolTipText("Press W");
        //interactableTooltip.gameObject.transform.localScale = Vector3.zero;
        interactableTooltip.gameObject.SetActive(true);
        //interactableTooltip.gameObject.LeanScale(new Vector3(1, 1, 1), .2f);
        showInteractableToolTip = true;
    }

    public virtual void Update()
    {
        if (showInteractableToolTip && interactableTooltip != null)
        {
            interactableTooltip.transform.position = Camera.main.WorldToScreenPoint(
                transform.position + interactableToolTipOffset);
        }
    }

    private void OnDestroy()
    {
        if (interactableTooltip != null)
        {
            Destroy(interactableTooltip.gameObject);
        }

    }
}
