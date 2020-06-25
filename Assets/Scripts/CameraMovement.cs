using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] float speed = 500f;
    [Tooltip("degree to rotate the camera")] [SerializeField] int degree = 10;

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
    }
    private void MoveCamera()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * speed,
                                               transform.position.y,
                                               transform.position.z + Input.GetAxis("Mouse Y") * speed
                                            );

            transform.position = newPosition;
        }
    }
    private void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * degree);
        }
    }
}
