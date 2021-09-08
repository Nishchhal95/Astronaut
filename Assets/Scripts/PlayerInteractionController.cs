using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 5f;
    [SerializeField] private SphereCollider interactionCollider;
    [SerializeField] private LayerMask whatIsInteractable;

    private void Awake()
    {
        interactionCollider.radius = interactionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (whatIsInteractable == (whatIsInteractable | (1 << other.gameObject.layer)))
        {
            if(other.gameObject.TryGetComponent(out Interactable interactable))
            {
                interactable.OnPlayerEnterInteractionZone();
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
            }
        }
    }
}
