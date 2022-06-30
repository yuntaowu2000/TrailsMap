using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class ColorPicker : MonoBehaviour
{
    private List<string> m_CountryNames = new List<string>{"Liberl", "Crossbell", "Erebonia", "Calvard"};
    private static Dictionary<string, Color32> m_ColorMap = new Dictionary<string, Color32>
    {
        {"Liberl", new Color32(0x00, 0xBF, 0xFf, 255)}, 
        {"Crossbell", new Color32(0xDC, 0xE9, 0xFE, 255)}, 
        {"Erebonia", new Color32(0xDC, 0x14, 0x3C, 255)},
        {"Calvard", new Color32(0x00, 0xFA, 0x9A, 255)},
        {"Nord", new Color32(0x7C,0xFC,0x00, 255)}
    };
    private List<Text> m_TextsInNameCanvas = new List<Text>();
    private List<Text> m_TextsInTitle = new List<Text>();
    [SerializeField] bool m_ShouldRefresh = false;
    private void Start() 
    {
        RefreshNameList();
    }
    // Update is called once per frame
    void Update()
    {
        if (m_ShouldRefresh) 
        {
            m_ShouldRefresh = false;
            RefreshNameList();
        }
        SetColorBasedOnCountry(transform.name);
    }

    private void RefreshNameList() 
    {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text text in texts) 
        {
            if (text.transform.parent.name == "NameCanvas") 
            {
                m_TextsInNameCanvas.Add(text);
                //Debug.Log("Name Canvas " + text.text);
            }
            else if (text.transform.name == "Title") 
            {
                m_TextsInTitle.Add(text);
                // Debug.Log("Title " + text.text);
            }
        }
    }

    private void SetColorBasedOnCountry(string country) {
        foreach (Text t in m_TextsInNameCanvas)
        {
            //Debug.Log ("Setting color for name " + t.text);
            string locationName = t.transform.parent.transform.parent.name;
            if (t == null)
                continue;
            t.color = m_ColorMap[country];
            if (!m_CountryNames.Contains(locationName))
                t.color = new Color32(m_ColorMap[country].r, m_ColorMap[country].g, m_ColorMap[country].b, 200);
            if (locationName == "Nord")
                t.color = m_ColorMap[locationName];
        }
        foreach (Text t in m_TextsInTitle)
        {
            // Debug.Log ("Setting color for title " + t.text);
            string locationName = t.transform.parent.transform.parent.name;
            if (locationName == "Nord")
                t.color = m_ColorMap[locationName];
            else
                t.color = m_ColorMap[country];
        }
    }
}
#endif