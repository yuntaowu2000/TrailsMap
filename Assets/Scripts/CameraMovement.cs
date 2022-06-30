using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float m_MinX = 0;
    private float m_MaxX = 0;
    private float m_MinZ = 0;
    private float m_MaxZ = 0;
    private float m_Height, m_RotX, m_RotZ;
    private Rigidbody m_CameraRb;

    //[SerializeField] float speed = 500f;
    [Tooltip("force to move the camera")] [SerializeField] float m_Force = 100f;
    [Tooltip("force to rotate the camera")] [SerializeField] float m_DegreeForce = 10;
    [SerializeField] Transform m_MapPosition = null;
    
    private void Start()
    {
        m_MinX = m_MapPosition.localPosition.x - 5.5f * m_MapPosition.localScale.x;
        m_MaxX = m_MapPosition.localPosition.x + 5.5f * m_MapPosition.localScale.x;
        m_MinZ = m_MapPosition.localPosition.z - 6.5f * m_MapPosition.localScale.z;
        m_MaxZ = m_MapPosition.localPosition.z + 6.5f * m_MapPosition.localScale.z;

        m_Height = transform.position.y;
        m_RotX = transform.rotation.eulerAngles.x;
        m_RotZ = transform.rotation.eulerAngles.z;

        m_CameraRb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (RotateCamera()) return;
        if (MoveCamera()) return;
    }

    private void LateUpdate() {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, m_MinX, m_MaxX),
            m_Height,
            Mathf.Clamp(transform.position.z, m_MinZ, m_MaxZ)
        );

        Vector3 newRotation = new Vector3(
            m_RotX, 
            transform.rotation.eulerAngles.y, 
            m_RotZ
        );
        transform.rotation = Quaternion.Euler(newRotation);
    }
    
    private bool MoveCamera()
    {
        if (Input.touchCount == 1) 
        {
            //cameraRb.angularVelocity = new Vector3(0,0,0);
            Touch t = Input.touches[0];
            if (t.deltaTime <= Time.deltaTime) 
            {
                Vector3 actualForce =  transform.forward * t.deltaPosition.y * m_Force / 5 
                                + transform.right * t.deltaPosition.x * m_Force / 5 ;
                m_CameraRb.AddForce(actualForce);
            }
            return true;
        }
        else if (Input.GetMouseButton(0))
        {
            m_CameraRb.angularVelocity = new Vector3(0,0,0);
            Vector3 actualForce = - transform.forward * Input.GetAxis("Mouse Y") * m_Force 
                                - transform.right * Input.GetAxis("Mouse X") * m_Force;
            m_CameraRb.AddForce(actualForce);
            return true;
        }
        return false;
    }
    private bool RotateCamera()
    {
        if (Input.touchCount == 2) 
        {
            m_CameraRb.velocity = new Vector3(0,0,0);
            Touch t = Input.touches[0];
            if (t.deltaTime <= Time.deltaTime)
            {
                m_CameraRb.AddTorque(transform.up * m_DegreeForce / 2.0f * t.deltaPosition.x);
            }
            return true;
        }
        else if (Input.GetMouseButton(1))
        {
            m_CameraRb.velocity = new Vector3(0,0,0);
            m_CameraRb.AddTorque(- transform.up * m_DegreeForce * Input.GetAxis("Mouse X"));
            //transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * degreeForce);
            return true;
        }
        return false;
    }
}
