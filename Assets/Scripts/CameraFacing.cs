using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour
{
    private Transform m_MainCameraTransform;
    void Start()
    {
        m_MainCameraTransform = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.forward = m_MainCameraTransform.forward;
    }
}
