using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;


public class BoxScriptIA : MonoBehaviour
{
    public int nPlayersNecesarios = 1;
    public TextMeshPro textoCaja;
    private Rigidbody2D caja;
    //Para quitar lag
    Vector3 networkPos;

    Rigidbody2D r;

    //Values that will be synced over network
    Vector3 latestPos;
    Quaternion latestRot;
    //Lag compensation
    float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
    Quaternion rotationAtLastPacket = Quaternion.identity;

    bool valuesReceived = false;

    private void Awake()
    {
        caja = GetComponent<Rigidbody2D>();
        networkPos = new Vector3();
        r = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        textoCaja.text = "" + nPlayersNecesarios;
    }

    public void changeNplayersSum()
    {
        nPlayersNecesarios++;
        if (nPlayersNecesarios == 1)
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Caja") && !SceneManager.GetActiveScene().name.Equals("Level7"))
        {
            transform.parent = collision.transform;

            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.anchor = collision.contacts[0].point;
            joint.connectedBody = collision.collider.GetComponent<Rigidbody2D>();
            joint.enableCollision = false;
            collision.transform.GetComponent<BoxScript>().changeNplayersRest();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            
        }

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();

            //Lag compensation
            currentTime = 0.0f;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
            positionAtLastPacket = transform.position;
            rotationAtLastPacket = transform.rotation;
        }
    }


}
