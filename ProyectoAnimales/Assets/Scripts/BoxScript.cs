using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; 


public class BoxScript : MonoBehaviourPun
{
    public int nPlayersNecesarios = 2;
    public TextMeshPro textoCaja;
    private Rigidbody2D caja;
    

    private void Awake()
    {
        caja = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        textoCaja.text = "" + nPlayersNecesarios;
    }
 /*   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //photonView.RPC("changeNplayersRest", RpcTarget.All);
            changeNplayersRest();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //photonView.RPC("changeNplayersSum", RpcTarget.All);
            changeNplayersSum();
        }
    }*/

    //[PunRPC]
    public void changeNplayersSum()
    {
        nPlayersNecesarios++;
        if (nPlayersNecesarios == 1)
        {
           // caja.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            caja.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

        }

    }

    //[PunRPC]
    public void changeNplayersRest()
    {
        nPlayersNecesarios--;
        if (nPlayersNecesarios == 0)
        {
            caja.constraints = RigidbodyConstraints2D.None;
            caja.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.CompareTag("Caja"))
        {
            //transform.parent = collision.transform;
            //collision.transform.position = new Vector3(collision.transform.position.x + 0.1f,collision.transform.position.y, 0f);
            //collision.GetComponent<BoxScript>().caja.bodyType = RigidbodyType2D.Kinematic;
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.anchor = collision.contacts[0].point;
            joint.connectedBody = collision.collider.GetComponent<Rigidbody2D>();
            joint.enableCollision = false;
        }
    }

}
