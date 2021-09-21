using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool showInteractableToolTip = false;
    [SerializeField] protected InteractableTooltip interactableTooltip;
    [SerializeField] protected Vector3 interactableToolTipOffset = new Vector3(0, .2f, 0);

    private float minX, maxX;
    public abstract void Interact();
    public virtual void OnPlayerEnterInteractionZone()
    {
        SetupInteractableToolTip();
    }
    public virtual void OnPlayerExitInteractionZone()
    {
        HideInteractableToolTip();
    }

    private void SetupInteractableToolTip()
    {
        if(interactableTooltip == null)
        {
            interactableTooltip = SpawnInteractableToolTip();
        }

        if (interactableTooltip.gameObject.activeSelf)
        {
            interactableTooltip.gameObject.SetActive(false);
        }
        SetTextOnInteractableToolTip(interactableTooltip);
    }

    private void HideInteractableToolTip()
    {
        //interactableTooltip.gameObject.LeanScale(Vector3.zero, .2f).setOnComplete(() =>
        //{
        //    interactableTooltip.gameObject.SetActive(false);
        //    interactableTooltip.SetToolTipText("");
        //});

        if (interactableTooltip.gameObject.activeSelf)
        {
            interactableTooltip.gameObject.SetActive(false);
        }
        interactableTooltip.SetToolTipText("");
        showInteractableToolTip = false;
    }

    private InteractableTooltip SpawnInteractableToolTip()
    {
        return Instantiate(ItemReferencer.Instance.interactableToolTipPrefab, ItemReferencer.Instance.interactableToolTipCanvasTransform);
    }

    private void SetTextOnInteractableToolTip(InteractableTooltip interactableTooltip)
    {
        interactableTooltip.SetToolTipText("Press I");
        //interactableTooltip.gameObject.transform.localScale = Vector3.zero;
        ShowInteractableToolTip();
    }

    private void ShowInteractableToolTip()
    {
        if (!interactableTooltip.gameObject.activeSelf)
        {
            interactableTooltip.gameObject.SetActive(true);
        }
        //interactableTooltip.gameObject.LeanScale(new Vector3(1, 1, 1), .2f);
        showInteractableToolTip = true;
    }

    public virtual void Update()
    {
        if (showInteractableToolTip && interactableTooltip != null)
        {
            Vector3 toolTipScreenPos = GameReferenceManager.Instance.playerCamera.WorldToScreenPoint(
                transform.position + interactableToolTipOffset);
            if (Vector3.Dot(
                interactableTooltip.transform.position - GameReferenceManager.Instance.playerCameraTransform.position,
                GameReferenceManager.Instance.playerCameraTransform.forward) < 0)
            {
                HideInteractableToolTip();
                return;
            }

            ShowInteractableToolTip();
            interactableTooltip.transform.position = toolTipScreenPos;
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
