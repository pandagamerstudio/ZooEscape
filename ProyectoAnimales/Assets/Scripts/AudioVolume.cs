using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    private AudioSource audioSrc;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("music") == false)
        {
            PlayerPrefs.SetFloat("music", 0.5f);
        }
        audioSrc.volume = PlayerPrefs.GetFloat("music");

        if (PlayerPrefs.HasKey("sfx") == false)
        {
            PlayerPrefs.SetFloat("sfx", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = PlayerPrefs.GetFloat("music");
    }

    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("music", vol);
    }

    public void SetSfxOn()
    {
        PlayerPrefs.SetFloat("sfx", 1f);
    }

    public void SetSfxOff()
    {
        PlayerPrefs.SetFloat("sfx", 0f);
    }
}
