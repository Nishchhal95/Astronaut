using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class GameReferenceManager : MonoBehaviour
{
    public static GameReferenceManager Instance = null;

    public Transform playerTransform, playerCameraTransform, playerCenterPointTransform;
    public Camera playerCamera;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetupReferences();
    }

    private void SetupReferences()
    {
        playerTransform = FindObjectOfType<ThirdPersonController>().transform;
        playerCamera = Camera.main;
        playerCameraTransform = playerCamera.transform;
    }
}
