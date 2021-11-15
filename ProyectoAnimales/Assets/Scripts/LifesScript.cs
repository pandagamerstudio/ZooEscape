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
    }

    //[PunRPC]
    public void LoseLife()
    {
        if (livesRemaining == 0)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Menu");
            }
            
        }

        livesRemaining--;
        PlayerPrefs.SetInt("livesRemaining", livesRemaining);

        Debug.Log(livesRemaining);

        UpdateLivesUI();
    }

    private void UpdateLivesUI()
    {
        Debug.Log("Enta ui");
        for(int i=livesRemaining-1;i<=0;i--)
        {
            lives[i].enabled = false;
        }
    }
}
