using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LifesScript : MonoBehaviourPun
{
    public Image[] lives;
    public int livesRemaining;
    private Color cora = new Color(1f, 1f, 1f, 0f);
    private int entraUi = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("entraUi"))
        {
            entraUi = PlayerPrefs.GetInt("entraUi");
        }
        if (PlayerPrefs.HasKey("livesRemaining"))
        {
            livesRemaining = PlayerPrefs.GetInt("livesRemaining");
        }
        Debug.Log(livesRemaining);

        if(entraUi == 1)
        {
            UpdateLivesUI();
        }
        
        PlayerPrefs.SetInt("entraUi", 1);
    }

    //[PunRPC]
    public void LoseLife()
    {
        if (livesRemaining == 0)
        {
            Debug.Log("vidas0");
            if(PhotonNetwork.IsMasterClient)
            {
                Debug.Log("camvbionivel");
                PhotonNetwork.LoadLevel("Menu");
            }
            
        }

        livesRemaining--;
        PlayerPrefs.SetInt("livesRemaining", livesRemaining);

        Debug.Log(livesRemaining);
    }

    private void UpdateLivesUI()
    {
        Debug.Log("Enta ui");

        int aux = livesRemaining;

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
        }
    }
}
