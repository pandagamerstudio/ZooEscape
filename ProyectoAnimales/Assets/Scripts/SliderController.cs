using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider barra;
    public int numBarra;

    // Start is called before the first frame update
    void Start()
    {
        if (numBarra == 0)
        {
            barra.value = PlayerPrefs.GetFloat("music");
        }
        else
        {
            barra.value = PlayerPrefs.GetFloat("sfx");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(numBarra == 0)
        {
            PlayerPrefs.SetFloat("music", barra.value);
        }
        else
        {
            PlayerPrefs.SetFloat("sfx", barra.value);
        }
        
        
    }
}
