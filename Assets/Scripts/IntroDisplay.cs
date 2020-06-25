using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDisplay : MonoBehaviour
{
    [SerializeField] Canvas relatedIntro = null;
    [SerializeField] CameraMovement cameraControl = null;
    private void OnMouseOver()
    {
        Debug.Log("Mouse entered");
        if (relatedIntro == null) return;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            cameraControl.enabled = false;
            relatedIntro.enabled = true;
        }
    }

    public void CloseIntro()
    {
        cameraControl.enabled = true;
        relatedIntro.enabled = false;
    }
}
