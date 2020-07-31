﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] float speed = 500f;
    [SerializeField] float force = 100f;
    [SerializeField] Transform mapPosition = null;
    float minX = 0;
    float maxX = 0;
    float minZ = 0;
    float maxZ = 0;
    float height;
    [Tooltip("degree to rotate the camera")] [SerializeField] int degree = 10;
    Rigidbody cameraRb;

    private void Start()
    {
        minX = mapPosition.localPosition.x - 5.5f * mapPosition.localScale.x;
        maxX = mapPosition.localPosition.x + 5.5f * mapPosition.localScale.x;
        minZ = mapPosition.localPosition.z - 5.5f * mapPosition.localScale.z;
        maxZ = mapPosition.localPosition.z + 5.5f * mapPosition.localScale.z;
        height = transform.position.y;
        cameraRb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    private void LateUpdate() {

        Vector3 newPosition = new Vector3();
        newPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
        newPosition.y = height;
        newPosition.z = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = newPosition;
    }
    
    private void MoveCamera()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 actualForce = - transform.forward * Input.GetAxis("Mouse Y") * force 
                                - transform.right * Input.GetAxis("Mouse X") * force;
            // actualForce.y = 0;
            // Vector3 newPosition = transform.position
            //                         - transform.forward * Input.GetAxis("Mouse Y") * speed
            //                         - transform.right * Input.GetAxis("Mouse X") * speed;
            cameraRb.AddForce(actualForce);
        }
    }
    private void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            cameraRb.velocity = new Vector3(0,0,0);
            transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * degree);
        }
    }
}
