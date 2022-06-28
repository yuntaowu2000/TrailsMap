using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AudioChange : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private Text BGMText = null;
    [SerializeField] private Dropdown dropdown = null;

    private IEnumerator currCoroutine = null;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (BGMText != null) {
            BGMText.text = string.Format("BGM: None");
        }
    }

    public void SetPause()
    {
        if (audioSource.isPlaying) {
            audioSource.Pause();
            BGMText.text = "BGM: Paused";
        } else if (audioSource.clip != null) {
            audioSource.Play();
            BGMText.text = "BGM: Playing";
        }
    }

    public void SetBGM(int dropdownValue) {

        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
        if (currCoroutine != null) {
            StopCoroutine(currCoroutine);
        }

        if (dropdownValue == 0) {
            if (BGMText != null) {
                BGMText.text = string.Format("BGM: None");
            }
            audioSource.clip = null;
            currCoroutine = null;
            return;
        }
        
        currCoroutine = GetBGM(dropdownValue);
        StartCoroutine(currCoroutine);
    }

    private IEnumerator GetBGM(int dropdownValue) {
        string url = string.Format("https://alioss.trails-game.com/audio/map/{0}.mp3", dropdownValue);
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
                BGMText.text = string.Format("BGM: Downloading {0:0}%", uwr.downloadProgress * 100);
                yield return null;
            }
            
            if (BGMText != null) {
                BGMText.text = string.Format("BGM: Playing");
            }
            audioSource.clip = dlHandler.audioClip;
            audioSource.Play();
        }

    }
}
