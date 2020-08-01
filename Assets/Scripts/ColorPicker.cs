using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ColorPicker : MonoBehaviour
{

    List<string> countryName = new List<string>();
    Color32[] colors = {new Color32(0x00, 0xBF, 0xFf, 255), 
                    new Color32(0xDC, 0xE9, 0xFE, 255), 
                    new Color32(0xDC, 0x14, 0x3C, 255)};

    Text[] texts = null;
    List<Text> textsInNameCanvas = new List<Text>();
    List<Text> textsInTitle = new List<Text>();
    private void Awake() {
        
        countryName.Add("Liberl");
        countryName.Add("Crossbell");
        countryName.Add("Erebonia");
        texts = GetComponentsInChildren<Text>();
        foreach(Text text in texts) {
            if (text.transform.parent.name == "NameCanvas") {
                textsInNameCanvas.Add(text);
                //Debug.Log("Name Canvas " + text.text);
            }
            else if (text.transform.name == "Title") {
                textsInTitle.Add(text);
                //Debug.Log("Title " + text.text);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.name == "Liberl") {
            SetColorBasedOnCountry(0);
        }else if (transform.name == "Crossbell") {
            SetColorBasedOnCountry(1);
        } else if (transform.name == "Erebonia")
        {
            SetColorBasedOnCountry(2);
        }
    }

    private void SetColorBasedOnCountry(int i) {
        foreach (Text t in textsInNameCanvas)
        {
            //Debug.Log ("Setting color for name " + t.text);
            
            
            if (countryName.Contains(t.transform.parent.transform.parent.name)) {
                t.color = new Color32(colors[i].r, colors[i].g, colors[i].b, 255);
            } else {
                t.color = new Color32(colors[i].r, colors[i].g, colors[i].b, 200);
            }
            if (t.transform.parent.transform.parent.name == "Nord") {
                t.color = new Color32(0x7C,0xFC,0x00, 255);
            }
        }
        foreach (Text t in textsInTitle)
        {
            //Debug.Log ("Setting color for title " + t.text);
            t.color = colors[i];
            if (t.transform.parent.transform.parent.name == "Nord") {
                t.color = new Color32(0x7C,0xFC,0x00, 255);
            }
        }
    }
}