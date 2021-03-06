﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    //[SerializeField] float speed = 500f;
    [SerializeField] float force = 100f;
    [SerializeField] Transform mapPosition = null;
    float minX = 0;
    float maxX = 0;
    float minZ = 0;
    float maxZ = 0;
    float height, rotX, rotZ;
    [Tooltip("force to rotate the camera")] [SerializeField] float degreeForce = 10;
    Rigidbody cameraRb;

    private void Start()
    {
        minX = mapPosition.localPosition.x - 5.5f * mapPosition.localScale.x;
        maxX = mapPosition.localPosition.x + 5.5f * mapPosition.localScale.x;
        minZ = mapPosition.localPosition.z - 6.5f * mapPosition.localScale.z;
        maxZ = mapPosition.localPosition.z + 6.5f * mapPosition.localScale.z;

        height = transform.position.y;
        rotX = transform.rotation.eulerAngles.x;
        rotZ = transform.rotation.eulerAngles.z;

        cameraRb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (RotateCamera()) return;
        if (MoveCamera()) return;
    }

    private void LateUpdate() {

        Vector3 newPosition = new Vector3();
        newPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
        newPosition.y = height;
        newPosition.z = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = newPosition;

        Vector3 newRotation = new Vector3();
        newRotation.x = rotX;
        newRotation.z = rotZ;
        newRotation.y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(newRotation);
    }
    
    private bool MoveCamera()
    {
        if (Input.touchCount == 1) {
            //cameraRb.angularVelocity = new Vector3(0,0,0);
            Touch t = Input.touches[0];
            if (t.deltaTime <= Time.deltaTime) {
                Vector3 actualForce =  transform.forward * t.deltaPosition.y * force / 5 
                                + transform.right * t.deltaPosition.x * force / 5 ;
                cameraRb.AddForce(actualForce);
            }
            return true;
        }
        else if (Input.GetMouseButton(0))
        {
            cameraRb.angularVelocity = new Vector3(0,0,0);
            Vector3 actualForce = - transform.forward * Input.GetAxis("Mouse Y") * force 
                                - transform.right * Input.GetAxis("Mouse X") * force;
            cameraRb.AddForce(actualForce);
            return true;
        }
        return false;
    }
    private bool RotateCamera()
    {
        if (Input.touchCount == 2) {
            cameraRb.velocity = new Vector3(0,0,0);
            Touch t = Input.touches[0];
            if (t.deltaTime <= Time.deltaTime)
            {
                cameraRb.AddTorque(transform.up * degreeForce / 2.0f * t.deltaPosition.x);
            }
            return true;
        }
        else if (Input.GetMouseButton(1))
        {
            cameraRb.velocity = new Vector3(0,0,0);
            cameraRb.AddTorque(- transform.up * degreeForce * Input.GetAxis("Mouse X"));
            //transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * degreeForce);
            return true;
        }
        return false;
    }
}
