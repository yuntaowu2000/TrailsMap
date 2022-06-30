using UnityEngine;
using System.Runtime.InteropServices;

public class LinkOpener : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenWebPage(string link);

    private string m_WebLink;

    private void Start()
    {
        string locationName = transform.parent.name;
        m_WebLink = "https://trails-game.com/region/" + locationName;
    }

    public void OpenWebPage()
    {
#if UNITY_EDITOR
        Debug.Log($"trying to open {m_WebLink}");
#else
        OpenWebPage(m_WebLink);
#endif
    }
}
