using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDisplay : MonoBehaviour
{
    [SerializeField] Canvas relatedIntro = null;
    [SerializeField] CameraMovement cameraControl = null;
    [SerializeField] Texture2D cursorTexture = null;

    IntroDisplay[] allIntroDisplays = null;
    bool opened = false;

    private void Start()
    {
        allIntroDisplays = FindObjectsOfType<IntroDisplay>();
    }

    //called when mouse hover onto the object
    private void OnMouseOver()
    {
        Debug.Log("Mouse entered");
        if (opened) return;
        if (relatedIntro == null) return;
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(), CursorMode.Auto);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            opened = true;
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
