using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlatMovManager : MonoBehaviourPun
{
    public GameObject plataforma;
    private PlatformMovement platMov;
    
    // Start is called before the first frame update
    void Start()
    {
        platMov = plataforma.GetComponent<PlatformMovement>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(platMov.activate);

        if (!PhotonNetwork.IsMasterClient)
            return;

        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            photonView.RPC("ActivatePlatform", RpcTarget.All);
        }
    }

    [PunRPC]
    void ActivatePlatform()
    {
        platMov.activate = true;
    }
}
