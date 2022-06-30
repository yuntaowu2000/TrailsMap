using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDisplay : MonoBehaviour
{
    private CameraMovement cameraControl = null;
    private Rigidbody cameraRb;
    private bool opened = false;
    private IntroDisplay[] allIntroDisplays = null;
    [SerializeField] Canvas relatedIntro = null;
    
    [SerializeField] Texture2D cursorTexture = null;

    private void Start()
    {
        allIntroDisplays = FindObjectsOfType<IntroDisplay>();
        cameraControl = FindObjectOfType<CameraMovement>();
        cameraRb = cameraControl.GetComponent<Rigidbody>();
    }

    //called when mouse hover onto the object
    private void OnMouseOver()
    {
        //Debug.Log("Mouse entered");
        if (opened) return;
        if (relatedIntro == null) return;
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(), CursorMode.Auto);
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clicked");
            opened = true;
            cameraRb.angularVelocity = Vector3.zero;
            cameraRb.velocity = Vector3.zero;
            cameraControl.enabled = false;
            relatedIntro.enabled = true;
            SetOtherIntroDisplay(false);
        }
    }

    private void SetOtherIntroDisplay(bool active)
    {
        foreach (IntroDisplay introDisplay in allIntroDisplays)
        {
            if (introDisplay != this)
            {
                introDisplay.enabled = active;
                introDisplay.gameObject.GetComponent<Collider>().enabled = active;
            }
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
    }

    public void CloseIntro()
    {
        opened = false;
        cameraControl.enabled = true;
        relatedIntro.enabled = false;
        SetOtherIntroDisplay(true);
    }
}
