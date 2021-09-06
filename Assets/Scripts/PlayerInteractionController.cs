using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Vector3 interactionBoxSize = new Vector3(3, 3, 3);
    [SerializeField] private bool debugMode = false;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private GameObject interactableGO;
    [SerializeField] private IInteractable interactable;
    [SerializeField] private IInteractable lastInteractable;
    [SerializeField] private string interactionKey = "Q";

    private void Update()
    {
        CheckAndUpdateNearbyInteractable();
    }

    private void CheckAndUpdateNearbyInteractable()
    {
        if(interactable == null)
        {
            lastInteractable = interactable;
            InteractableToolTipManager.onInteractableOutOfRange?.Invoke();
        }
        interactable = GetInteractableNearby();
        //Debug.Log("Interactable " + (interactable == null ? " NULL " : "SOMETHING"));

        if(interactable != null && interactable != lastInteractable)
        {
            InteractableToolTipManager.onInteractableInRange?.Invoke(interactableGO, interactionKey);
        }
    }

    private IInteractable GetInteractableNearby()
    {
        IInteractable interactable = null;
        BoxCastInfo boxCastInfo = FireAndGetBoxCastInfo();
        if (boxCastInfo.hit && boxCastInfo.hitInfo.collider != null && boxCastInfo.hitInfo.collider.gameObject != null)
        {
            GameObject gameObjectNearby = boxCastInfo.hitInfo.collider.gameObject;
            interactable = boxCastInfo.hitInfo.collider.gameObject.GetComponent<IInteractable>();
            interactableGO = boxCastInfo.hitInfo.collider.gameObject;
        }

        return interactable;
    }

    private BoxCastInfo FireAndGetBoxCastInfo()
    {
        BoxCastInfo boxCastInfo = new BoxCastInfo();
        RaycastHit[] hitInfos = Physics.BoxCastAll(transform.position, interactionBoxSize / 2, Vector3.forward,
            Quaternion.identity, 0, whatIsInteractable);
        if (hitInfos != null && hitInfos.Length > 0)
        {
            boxCastInfo.hit = true;
            boxCastInfo.hitInfo = hitInfos[0];
        }
        return boxCastInfo;
    }

    private void OnDrawGizmos()
    {
        if (!debugMode)
        {
            return;
        }
        Gizmos.DrawWireCube(transform.position, interactionBoxSize);
    }
}

public class BoxCastInfo
{
    public bool hit;
    public RaycastHit hitInfo;
}
