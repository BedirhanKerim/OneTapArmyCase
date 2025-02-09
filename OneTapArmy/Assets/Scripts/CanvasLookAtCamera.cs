using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = _mainCamera.transform.forward;
    }
}
