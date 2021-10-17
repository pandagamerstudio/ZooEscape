using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Rope : MonoBehaviour
{

    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i< numLinks; i++)
        {
            GameObject newSeg = PhotonNetwork.Instantiate(prefabRopeSegs[i].name, new Vector3(0,0,0), Quaternion.identity);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
