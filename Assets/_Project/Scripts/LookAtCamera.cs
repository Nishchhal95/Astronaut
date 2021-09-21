using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        transform.forward = playerCamera.transform.position - transform.position;
    }
}
