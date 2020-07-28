using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    public void OpenWebPage()
    {
        string locationName = transform.parent.name;
        string weblink = "'https://trails-game.com/map/" + locationName + "'";
        Application.ExternalEval("w=window.open("+weblink+",'_blank')");
    }
}
