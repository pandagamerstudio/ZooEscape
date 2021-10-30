using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class recargarEscena : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // PhotonNetwork.LoadScene("SceneName");
         
           
            PhotonNetwork.LoadLevel("Level1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
