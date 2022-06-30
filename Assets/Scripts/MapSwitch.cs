using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSwitch : MonoBehaviour
{
    [SerializeField] Button m_SwitchButton = null;
    [SerializeField] Text m_SwitchButtonText = null;
    [SerializeField] GameObject m_Map1 = null;
    [SerializeField] GameObject m_Map2 = null;

    private bool m_Map1Enabled = true;
    void Start()
    {
        m_SwitchButtonText.text = "切换至东部地图";
        m_SwitchButton.onClick.AddListener(()=>OnSwitchClick());
    }

    private void OnSwitchClick() 
    {
        if (m_Map1Enabled) 
        {
            m_Map1.SetActive(false);
            m_Map2.SetActive(true);
            m_SwitchButtonText.text = "切换至西部地图";
        } 
        else 
        {
            m_Map1.SetActive(true);
            m_Map2.SetActive(false);
            m_SwitchButtonText.text = "切换至东部地图";
        }
        m_Map1Enabled = !m_Map1Enabled;
    }
    
}
