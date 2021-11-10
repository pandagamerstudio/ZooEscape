using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class recargarScene : MonoBehaviourPun
{

    bool aux;
    // Start is called before the first frame update
    void Start()
    {

        aux = true;
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(reiniciar());
        }

    }

    IEnumerator reiniciar()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel("Level3");

    }
    void pasarEscena()
    { 
    
    
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2&& aux)
        {
            aux = false;
        }

    }
}
