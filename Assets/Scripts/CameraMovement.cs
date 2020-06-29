using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] float speed = 500f;
    [SerializeField] Transform mapPosition;
    float minX = 0;
    float maxX = 0;
    float minZ = 0;
    float maxZ = 0;
    float height;
    [Tooltip("degree to rotate the camera")] [SerializeField] int degree = 10;

    private void Start()
    {
        minX = mapPosition.localPosition.x - 6f * mapPosition.localScale.x;
        maxX = mapPosition.localPosition.x + 6f * mapPosition.localScale.x;
        minZ = mapPosition.localPosition.z - 6f * mapPosition.localScale.z;
        maxZ = mapPosition.localPosition.z + 6f * mapPosition.localScale.z;
        height = transform.position.y;
    }
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
            Vector3 newPosition = transform.position
                                    - transform.forward * Input.GetAxis("Mouse Y") * speed
                                    - transform.right * Input.GetAxis("Mouse X") * speed;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = height;
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
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
