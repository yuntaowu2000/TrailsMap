using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioChange : MonoBehaviour
{
    // Start is called before the first frame update
    string musicVersion = null;
    [SerializeField] AudioClip[] audioClip = null;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVersion() {
        //get the name of the button and set it as destination scene
        string newMusicVersion = EventSystem.current.currentSelectedGameObject.name;
        if (newMusicVersion == musicVersion) return;
        musicVersion = newMusicVersion;
        switch (musicVersion)
        {
            case "FC": 
                audioSource.clip = audioClip[0];
                Debug.Log("FC clicked");
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
