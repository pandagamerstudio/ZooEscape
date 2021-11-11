using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class cargarEscena : MonoBehaviourPun
{

    bool aux;
    string scene;
    // Start is called before the first frame update
    void Start()
    {

        aux = true;
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(reiniciar());
            scene = PlayerPrefs.GetString ("Scene");
        }

    }

    IEnumerator reiniciar()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel(scene);

    }
    void pasarEscena()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2 && aux)
        {
            aux = false;
        }

    }
}
