using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            default:
                audioSource.clip = audioClip[0];
                break;
        }
        audioSource.Play();
    }
}
