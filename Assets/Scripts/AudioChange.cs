using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AudioChange : MonoBehaviour
{
    // string musicVersion = null;
    // string[] musicNames = {"wind_from_liberl.mp3", "116_Water_Plants_and_the_Blue_Sky.mp3", "1-21_Beyond_the_Drifting_Clouds.mp3", "ed9999.mp3"};
    private AudioSource audioSource;
    [SerializeField] private Text BGMText = null;
    [SerializeField] private Dropdown dropdown = null;
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
        if (dropdownValue == 0) {
            if (BGMText != null) {
                BGMText.text = string.Format("BGM: None");
            }
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
            audioSource.clip = null;
            return;
        }
        StartCoroutine(GetBGM(dropdownValue));
    }

    private IEnumerator GetBGM(int dropdownValue) {
        string url = string.Format("https://data.trails-game.com/musics/{0}.mp3", dropdownValue);
        Debug.Log(url);

        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG)) {
            
            DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;
            dlHandler.streamAudio = true;

            var operation = uwr.SendWebRequest();

            while (!operation.isDone) {
                BGMText.text = string.Format("BGM: Downloading {0:0}%", uwr.downloadProgress * 100);
                yield return null;
            }
            
            if (uwr.isNetworkError || uwr.isHttpError) {
                Debug.Log(uwr.error);
                yield break;
            }

            audioSource.clip = DownloadHandlerAudioClip.GetContent(uwr);
            audioSource.Play();
            if (BGMText != null) {
                BGMText.text = string.Format("BGM: Playing");
            }
        }

    }

    // public void SetVersion() {
    //     //get the name of the button and set it as destination scene
    //     string newMusicVersion = EventSystem.current.currentSelectedGameObject.name;
    //     if (newMusicVersion == musicVersion) return;
    //     musicVersion = newMusicVersion;
    //     switch (musicVersion)
    //     {
    //         case "Sora": 
    //             StartCoroutine(GetBGM(musicNames[0]));
    //             Debug.Log("Sora clicked");
    //             break;
    //         case "Ao":
    //             StartCoroutine(GetBGM(musicNames[1]));
    //             Debug.Log("Ao clicked");
    //             break;
    //         case "Sen":
    //             StartCoroutine(GetBGM(musicNames[2]));
    //             Debug.Log("Sen clicked");
    //             break;
    //         case "Akatsuki":
    //             StartCoroutine(GetBGM(musicNames[3]));
    //             Debug.Log("test clicked");
    //             break;
    //         default:
    //             StartCoroutine(GetBGM(musicNames[0]));
    //             break;
    //     }
    // }
}
