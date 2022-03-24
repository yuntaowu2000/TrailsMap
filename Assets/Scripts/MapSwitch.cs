using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSwitch : MonoBehaviour
{
    [SerializeField] Button switchButton = null;
    [SerializeField] Text switchButtonText = null;
    [SerializeField] GameObject map1 = null;
    [SerializeField] GameObject map2 = null;

    private bool map1Enabled = true;
    void Start()
    {
        switchButtonText.text = "切换至东部地图";
        switchButton.onClick.AddListener(()=>OnSwitchClick());
    }

    private void OnSwitchClick() {
        if (map1Enabled) {
            map1.SetActive(false);
            map2.SetActive(true);
            switchButtonText.text = "切换至西部地图";
        } else {
            map1.SetActive(true);
            map2.SetActive(false);
            switchButtonText.text = "切换至东部地图";
        }
        map1Enabled = !map1Enabled;
    }
    
}
