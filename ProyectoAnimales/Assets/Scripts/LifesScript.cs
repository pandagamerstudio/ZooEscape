using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LifesScript : MonoBehaviourPun
{
    public Image[] lives;
    public int livesRemaining;
    void Start()
    {
        if (PlayerPrefs.HasKey("livesRemaining"))
        {
            livesRemaining = PlayerPrefs.GetInt("livesRemaining");
        }
        Debug.Log(livesRemaining);

        UpdateLivesUI();
    }

    public void LoseLife()
    {
        livesRemaining--;
        PlayerPrefs.SetInt("livesRemaining", livesRemaining);

        Debug.Log(livesRemaining);
    }

    private void UpdateLivesUI()
    {
        Debug.Log("Enta ui");

        int aux = livesRemaining;

    /*
        while(aux > 0)
        {
            if(livesRemaining == 3)
            {
                lives[3].enabled = false;
            }
            if (livesRemaining == 2)
            {
                lives[3].enabled = false;
                lives[2].enabled = false;
            }
            if (livesRemaining == 1)
            {
                lives[3].enabled = false;
                lives[2].enabled = false;
                lives[1].enabled = false;
            }
            aux--;
        }*/

        if(livesRemaining == 3)
        {
            lives[3].enabled = false;
        }
        if (livesRemaining == 2)
        {
            lives[3].enabled = false;
            lives[2].enabled = false;
        }
        if (livesRemaining == 1)
        {
            lives[3].enabled = false;
            lives[2].enabled = false;
            lives[1].enabled = false;
        }
    }
}
