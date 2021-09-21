using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMat : MonoBehaviour
{
    [SerializeField] private Door door;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        door.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        door.Close();
    }
}
