using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform topDoorTransform, bottomDoorTransform;
    [SerializeField] private Vector3 topDoorOpenTargetPosition, bottomDoorOpenTargetPosition;
    [SerializeField] private float doorOpenDuration = 1f, doorCloseDuration = 2f;
    private Vector3 _topDoorCloseTargetPosition, _bottomDoorCloseTargetPosition, 
        _topDoorInitialPos, _bottomDoorInitialPos;

    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    private bool isOpen, inProcess;

    private void Start()
    {
        _topDoorCloseTargetPosition = topDoorTransform.localPosition;
        _bottomDoorCloseTargetPosition = bottomDoorTransform.localPosition;
        _topDoorInitialPos = topDoorTransform.localPosition;
        _bottomDoorInitialPos = bottomDoorTransform.localPosition;
    }

    public void Open()
    {        
        if (inProcess)
        {
            return;
        }

        inProcess = true;
        StartCoroutine(ProcessDoorOpenClose(topDoorOpenTargetPosition, 
            bottomDoorOpenTargetPosition, doorOpenDuration));
    }

    public void Close()
    {        
        if (inProcess)
        {
            return;
        }

        inProcess = true;
        StartCoroutine(ProcessDoorOpenClose(_topDoorCloseTargetPosition, 
            _bottomDoorCloseTargetPosition, doorCloseDuration));
    }

    private IEnumerator ProcessDoorOpenClose(Vector3 topDoorTargetPos, Vector3 bottomDoorTargetPos, float doorTransitionTime)
    {
        float elapsed = 0;
        while (elapsed < 1)
        {
            topDoorTransform.localPosition = Vector3.Lerp(_topDoorInitialPos, topDoorTargetPos, 
                elapsed / doorTransitionTime);
            bottomDoorTransform.localPosition = Vector3.Lerp(_bottomDoorInitialPos, bottomDoorTargetPos,
                elapsed / doorTransitionTime);
            elapsed += Time.deltaTime;
            yield return _waitForEndOfFrame;
        }
        
        topDoorTransform.localPosition = topDoorTargetPos;
        bottomDoorTransform.localPosition = bottomDoorTargetPos;
        
        _topDoorInitialPos = topDoorTransform.localPosition;
        _bottomDoorInitialPos = bottomDoorTransform.localPosition;
        inProcess = false;
    }
}
