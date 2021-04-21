using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AudioChange : MonoBehaviour
{
    string musicVersion = null;
    [SerializeField] AudioClip[] audioClip = null;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVersion() {
        //get the name of the button and set it as destination scene
        string newMusicVersion = EventSystem.current.currentSelectedGameObject.name;
        if (newMusicVersion == musicVersion) return;
        musicVersion = newMusicVersion;
        switch (musicVersion)
        {
            case "Sora": 
                audioSource.clip = audioClip[0];
                Debug.Log("Sora clicked");
                break;
            case "Ao":
                audioSource.clip = audioClip[1];
                Debug.Log("Ao clicked");
                break;
            case "Sen":
                audioSource.clip = audioClip[2];
                Debug.Log("Sen clicked");
                break;
            case "test":
                StartCoroutine(GetBGM());
                Debug.Log("test clicked");
                break;
            default:
                audioSource.clip = audioClip[0];
                break;
        }
        audioSource.Play();
    }

    public void SetSilent()
    {
        audioSource.Stop();
    }

    private IEnumerator GetBGM() {
        string url = "https://raw.githubusercontent.com/yuntaowu2000/trails-game-models/master/ed9999.mp3";
        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG)) {

            DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;
            dlHandler.streamAudio = true;

            yield return uwr.SendWebRequest();
            
            if (uwr.isNetworkError || uwr.isHttpError) {
                Debug.Log(uwr.error);
                yield break;
            }

            if (dlHandler.isDone) {
                AudioClip audioClip = dlHandler.audioClip;
                if (audioClip != null) {
                    audioSource.clip = DownloadHandlerAudioClip.GetContent(uwr);
                    audioSource.Play();
                }
            }

        }

    }
}
