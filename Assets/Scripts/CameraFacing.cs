using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour
{
    void LateUpdate()
    {
        //GetComponent<RectTransform>().LookAt(Camera.main.transform, Vector3.up);
        transform.forward = Camera.main.transform.forward;
    }
}
