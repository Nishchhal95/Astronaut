using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : IPlayerController
{
    private float interactionRadius = 3f;
    private Transform transform;
    private Transform cameraTransform;
    private LayerMask whatIsInteractable;
    private float totalFOV = 90.0f;

    public PlayerInteractionController(Transform _transform, Transform _cameraTransform, float _interactionRadius, float _totalFOV, LayerMask _whatIsInteractable)
    {
        transform = _transform;
        cameraTransform = _cameraTransform;
        interactionRadius = _interactionRadius;
        totalFOV = _totalFOV;
        whatIsInteractable = _whatIsInteractable;
    }

    public void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius, whatIsInteractable);
        for (int i = 0; i < colliders.Length; i++)
        {
            Transform interactableObject = colliders[i].transform;
            Vector3 ditToObject = (interactableObject.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, ditToObject) < totalFOV / 2)
            {
                Debug.Log("I HIT SOMETHING " + i);
            }
        }
    }

    //NOT SURE IF WE NEED THIS FOV THING
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);

        Gizmos.color = Color.white;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * cameraTransform.forward;
        Vector3 rightRayDirection = rightRayRotation * cameraTransform.forward;
        Gizmos.DrawRay(cameraTransform.position, leftRayDirection * (interactionRadius * 2 + (Vector3.Distance(cameraTransform.position, transform.position))));
        Gizmos.DrawRay(cameraTransform.position, rightRayDirection * (interactionRadius * 2 + (Vector3.Distance(cameraTransform.position, transform.position))));
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }
}
