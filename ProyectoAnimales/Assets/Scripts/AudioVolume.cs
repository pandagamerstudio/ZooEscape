using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioVolume : MonoBehaviour
{
    public AudioSource audioSrc, sfxSrc;
    AudioClip sfxDerrota, sfxPasoNivel, sfxSeleccionar, sfxSalto, sfxLlave;
    

    // Start is called before the first frame update
    void Start()
    {
        sfxDerrota = Resources.Load<AudioClip>("Sonido/Efectos/SonidoDerrota-mejor");
        sfxPasoNivel = Resources.Load<AudioClip>("Sonido/Efectos/SonidoPasoNivel");
        sfxSalto = Resources.Load<AudioClip>("Sonido/Efectos/SonidoSalto");
        sfxSeleccionar = Resources.Load<AudioClip>("Sonido/Efectos/SonidoSeleccionar");
        sfxLlave = Resources.Load<AudioClip>("Sonido/Efectos/SonidoLlave");

        audioSrc.volume = PlayerPrefs.GetFloat("music");
        sfxSrc.volume = PlayerPrefs.GetFloat("sfx");
    }
    void Update(){
        audioSrc.volume = PlayerPrefs.GetFloat("music");
        sfxSrc.volume = PlayerPrefs.GetFloat("sfx");
    }
    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("music", vol);
    }

    public void SetSfx(float vol)
    {
        PlayerPrefs.SetFloat("sfx", vol);
    }

    public void playSfx(string a){
        switch(a){
            case "derrota":
                sfxSrc.PlayOneShot(sfxDerrota);
                break;
            case "pasoNivel":
                sfxSrc.PlayOneShot(sfxPasoNivel);
                break;
            case "seleccionar":
                sfxSrc.PlayOneShot(sfxSeleccionar);
                break;
            case "salto":
                sfxSrc.PlayOneShot(sfxSalto);
                break;
            case "llave":
                sfxSrc.PlayOneShot(sfxLlave);
                break;
            default:
                break;
        }
    }
}
