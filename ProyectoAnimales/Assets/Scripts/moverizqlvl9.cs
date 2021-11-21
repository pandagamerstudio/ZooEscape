using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class moverizqlvl9 : MonoBehaviourPun, IPunObservable
{
    Vector3 networkPosition;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameObject.transform.position);
            Debug.Log("Soy master");
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
                 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            transform.Translate(-0.05f, 0f, 0f);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, networkPosition, Time.deltaTime * 0.05f);

        }
    }
}
