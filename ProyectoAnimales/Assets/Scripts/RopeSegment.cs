using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RopeSegment : MonoBehaviourPun
{

    public GameObject connectedAbove, connectedBelow;
   
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC("UnirSegmentos", RpcTarget.All);   
    }

    [PunRPC]
    void UnirSegmentos()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();

        if (aboveSegment != null)
        {
            aboveSegment.connectedBelow = gameObject;
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, (spriteBottom * -0.5f) + 0.07f);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }

}
