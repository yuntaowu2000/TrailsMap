using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private IntroDisplay[] m_placeIntros = null;
    private Text m_introText = null;
    private Image m_backgroundImage = null;
    private CanvasGroup m_CanvasGroup = null;
    private CameraMovement m_CameraControl = null;

    [SerializeField] float m_WaitTime = 3f;
    [SerializeField] GameObject m_TutorialButton = null;
    [SerializeField] GameObject m_DirectStartButton = null;
    [SerializeField] GameObject m_SwitchCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        m_CameraControl = FindObjectOfType<CameraMovement>();
        m_CameraControl.enabled = false;
        m_introText = GetComponentInChildren<Text>();
        m_backgroundImage = GetComponentInChildren<Image>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_placeIntros = FindObjectsOfType<IntroDisplay>();
        SetPlaceIntroActive(false);
    }

    public void OnTutorialStartClicked()
    {
        HideButtons();
        StartCoroutine(TutorialFunction());
    }

    public void OnDirectStartClicked()
    {
        HideButtons();
        StartCoroutine(DirectStartFunction());
    }

    private void HideButtons()
    {
        m_TutorialButton.SetActive(false);
        m_DirectStartButton.SetActive(false);
    }

    private IEnumerator TutorialFunction()
    {
       
        m_introText.text = "按住鼠标左键/单指按住屏幕，并移动鼠标/手指来移动镜头位置";
        yield return new WaitForSeconds(m_WaitTime);
        m_introText.text = "按住鼠标右键/双指按住屏幕，并左右移动鼠标/手指来旋转镜头";
        yield return new WaitForSeconds(m_WaitTime);
        m_introText.text = "现在，开始游览西塞姆利亚大陆吧！";
        SetPlaceIntroActive(true);
        m_SwitchCanvas.SetActive(true);
        m_CameraControl.enabled = true;
        yield return FadeOut(m_WaitTime);
        Destroy(this.gameObject);
    }

    private IEnumerator DirectStartFunction()
    {
        m_CameraControl.enabled = true;
        SetPlaceIntroActive(true);
        m_SwitchCanvas.SetActive(true);
        yield return FadeOut(m_WaitTime);
        Destroy(this.gameObject);
    }

    private IEnumerator FadeOut(float time)
    {
        while (!Mathf.Approximately(0f, m_CanvasGroup.alpha))
        {
            m_CanvasGroup.alpha = Mathf.MoveTowards(m_CanvasGroup.alpha, 0f, Time.deltaTime / time);
            yield return null;
        }
    }

    private void SetPlaceIntroActive(bool active) 
    {
        foreach (IntroDisplay p in m_placeIntros) 
        {
            p.enabled = active;
        }
    }
}
