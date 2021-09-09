using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 5f;
    [SerializeField] private SphereCollider interactionCollider;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private float lookPercentageThreshold = 0.98f;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private HashSet<Interactable> interactables = new HashSet<Interactable>();

    private void Awake()
    {
        interactionCollider.radius = interactionRadius;
    }

    private void OnEnable()
    {
        StarterAssetsInputs.onInteractClick += PlayerInteract;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.onInteractClick -= PlayerInteract;
    }

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 20, Color.green);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (whatIsInteractable == (whatIsInteractable | (1 << other.gameObject.layer)))
        {
            if(other.gameObject.TryGetComponent(out Interactable interactable))
            {
                interactable.OnPlayerEnterInteractionZone();
                interactables.Add(interactable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (whatIsInteractable == (whatIsInteractable | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.TryGetComponent(out Interactable interactable))
            {
                interactable.OnPlayerExitInteractionZone();
                interactables.Remove(interactable);
            }
        }
    }

    private void PlayerInteract()
    {
        Interactable currentInteractableToInteract = null;
        foreach (Interactable item in interactables)
        {
            float lookPercentage = Vector3.Dot(playerCameraTransform.forward.normalized,
                (item.transform.position - playerCameraTransform.position).normalized);
            if(lookPercentage >= lookPercentageThreshold)
            {
                currentInteractableToInteract = item;
                break;
            }
        }

        if (currentInteractableToInteract != null)
        {
            currentInteractableToInteract.Interact();
        }
    }
}
