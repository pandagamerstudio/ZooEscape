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

        //photonView.RPC("UpdateLivesUI", RpcTarget.All);
        UpdateLivesUI();
    }

    //[PunRPC]
    public void LoseLife()
    {
        /*if (livesRemaining == 0)
        {
            Debug.Log("vidas0");
            if(PhotonNetwork.IsMasterClient)
            {
                Debug.Log("camvbionivel");
                PhotonNetwork.LoadLevel("Menu");
            }
            
        }*/

        livesRemaining--;
        PlayerPrefs.SetInt("livesRemaining", livesRemaining);

        Debug.Log(livesRemaining);
    }

    //[PunRPC]
    private void UpdateLivesUI()
    {
        Debug.Log("Vidas " + livesRemaining);

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
