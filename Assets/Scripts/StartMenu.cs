using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    
    [SerializeField] float waitTime = 3f;
    [SerializeField] CameraMovement cameraControl = null;
    
    [SerializeField] GameObject tutorialButton = null;
    [SerializeField] GameObject directStartButton = null;
    [SerializeField] GameObject switchCanvas = null;

    IntroDisplay[] placeIntros = null;
    Text introText = null;
    Image backgroundImage = null;
    CanvasGroup canvasGroup = null;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraControl.enabled = false;
        introText = GetComponentInChildren<Text>();
        backgroundImage = GetComponentInChildren<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        placeIntros = FindObjectsOfType<IntroDisplay>();
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
        tutorialButton.SetActive(false);
        directStartButton.SetActive(false);
    }

    private IEnumerator TutorialFunction()
    {
       
        introText.text = "按住鼠标左键/单指按住屏幕，并移动鼠标/手指来移动镜头位置";
        yield return new WaitForSeconds(waitTime);
        introText.text = "按住鼠标右键/双指按住屏幕，并左右移动鼠标/手指来旋转镜头";
        yield return new WaitForSeconds(waitTime);
        introText.text = "现在，开始游览西塞姆利亚大陆吧！";
        SetPlaceIntroActive(true);
        switchCanvas.SetActive(true);
        cameraControl.enabled = true;
        yield return FadeOut(waitTime);
        Destroy(this.gameObject);
    }

    private IEnumerator DirectStartFunction()
    {
        cameraControl.enabled = true;
        SetPlaceIntroActive(true);
        switchCanvas.SetActive(true);
        yield return FadeOut(waitTime);
        Destroy(this.gameObject);
    }

    private IEnumerator FadeOut(float time)
    {
        while (!Mathf.Approximately(0f, canvasGroup.alpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, Time.deltaTime / time);
            yield return null;
        }
    }

    private void SetPlaceIntroActive(bool active) {
        foreach (IntroDisplay p in placeIntros) {
            p.enabled = active;
        }
    }
}
