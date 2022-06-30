using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AudioController : MonoBehaviour
{
    private IEnumerator m_CurrentCoroutine = null;
    [SerializeField] private Text m_BGMText = null;
    [SerializeField] private Dropdown m_MusicSelectionDropDown = null;
    [SerializeField] private Button m_PlayPauseButton = null;
    [SerializeField] private AudioSource m_AudioSource;
    void OnValidate()
    {
        if (m_BGMText == null)
            Debug.LogError($"{nameof(m_BGMText)} is null");
        if (m_MusicSelectionDropDown == null)
            Debug.LogError($"{nameof(m_MusicSelectionDropDown)} is null");
        if (m_PlayPauseButton == null)
            Debug.LogError($"{nameof(m_PlayPauseButton)} is null");
        if (m_AudioSource == null)
            Debug.LogError($"{nameof(m_AudioSource)} is null");
    }
    void Start()
    {
        m_BGMText.text = "BGM: None";
        m_MusicSelectionDropDown.onValueChanged.AddListener(SetBGM);
        m_PlayPauseButton.onClick.AddListener(SetPause);
    }

    public void SetPause()
    {
        if (m_AudioSource.isPlaying) 
        {
            m_AudioSource.Pause();
            m_BGMText.text = "BGM: Paused";
        } 
        else if (m_AudioSource.clip != null) 
        {
            m_AudioSource.Play();
            m_BGMText.text = "BGM: Playing";
        }
    }

    public void SetBGM(int dropdownValue) 
    {

        if (m_AudioSource.isPlaying)
            m_AudioSource.Stop();
        if (m_CurrentCoroutine != null)
            StopCoroutine(m_CurrentCoroutine);

        if (dropdownValue == 0) 
        {
            m_BGMText.text = string.Format("BGM: None");
            m_AudioSource.clip = null;
            m_CurrentCoroutine = null;
            return;
        }
        
        m_CurrentCoroutine = GetBGM(dropdownValue);
        StartCoroutine(m_CurrentCoroutine);
    }

    private IEnumerator GetBGM(int dropdownValue) 
    {
        string url = $"https://alioss.trails-game.com/audio/map/{dropdownValue}.mp3";
        Debug.Log(url);

        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG)) {
            
            DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;
            dlHandler.streamAudio = true;

            var operation = uwr.SendWebRequest();

            // while (uwr.downloadedBytes < 102400 && !operation.isDone) {
            //     if (uwr.isNetworkError || uwr.isHttpError) {
            //         Debug.Log(uwr.error);
            //         yield break;
            //     }   
            //     BGMText.text = string.Format("BGM: Downloading {0:0}%", uwr.downloadProgress * 100);
            //     yield return null;
            // }

            while (!operation.isDone) {
                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError || uwr.result == UnityWebRequest.Result.DataProcessingError) {
                    Debug.Log(uwr.error);
                    yield break;
                }
                m_BGMText.text = string.Format("BGM: Downloading {0:0}%", uwr.downloadProgress * 100);
                yield return null;
            }
            
            if (m_BGMText != null) {
                m_BGMText.text = string.Format("BGM: Playing");
            }
            m_AudioSource.clip = dlHandler.audioClip;
            m_AudioSource.Play();
        }

    }
}
