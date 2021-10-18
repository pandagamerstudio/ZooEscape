using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Rope : MonoBehaviourPun
{

    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    GameObject newSeg;
    Rigidbody2D prevBod;
    public int numLinks = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        prevBod = hook;
        for (int i = 0; i< numLinks; i++)
        {  
           photonView.RPC("Cuerda", RpcTarget.All, i);
        }
    }

    [PunRPC]

    void Cuerda(int i)
    {
        newSeg = Instantiate(prefabRopeSegs[i]);
        newSeg.transform.parent = transform;
        newSeg.transform.position = transform.position;
        HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
        hj.connectedBody = prevBod;

        prevBod = newSeg.GetComponent<Rigidbody2D>();
    }
}
