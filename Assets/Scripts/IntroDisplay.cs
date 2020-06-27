using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDisplay : MonoBehaviour
{
    [SerializeField] Canvas relatedIntro = null;
    [SerializeField] CameraMovement cameraControl = null;
    [SerializeField] Texture2D cursorTexture = null;

    //called when mouse hover onto the object
    private void OnMouseOver()
    {
        Debug.Log("Mouse entered");
        if (relatedIntro == null) return;
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(), CursorMode.Auto);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            cameraControl.enabled = false;
            relatedIntro.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
    }

    public void CloseIntro()
    {
        cameraControl.enabled = true;
        relatedIntro.enabled = false;
    }
}
