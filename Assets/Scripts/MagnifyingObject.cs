using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingObject : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Vector3 screenPoint = playerCamera.WorldToScreenPoint(transform.position);
        screenPoint.x = screenPoint.x / Screen.width;
        screenPoint.y = screenPoint.y / Screen.height;
        _renderer.material.SetVector("_ObjScreenPos", screenPoint);
    }
}
