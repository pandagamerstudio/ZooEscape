using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;


public class BoxScript : MonoBehaviourPun, IPunObservable
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
        //if (nPlayersNecesarios < 0)
        //{
        //    nPlayersNecesarios = 0;
        //}
        textoCaja.text = "" + nPlayersNecesarios;

        if (!photonView.IsMine)
        {
            //Lag compensation
            double timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;

            //Update remote player
            //  transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, (float)(currentTime / timeToReachGoal));
            //  transform.rotation = Quaternion.Lerp(rotationAtLastPacket, latestRot, (float)(currentTime / timeToReachGoal));

           // transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            //   transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Caja") && !SceneManager.GetActiveScene().name.Equals("Level7"))
        {
            transform.parent = collision.transform;
            //collision.transform.position = new Vector3(collision.transform.position.x + 0.1f,collision.transform.position.y, 0f);
            //collision.GetComponent<BoxScript>().caja.bodyType = RigidbodyType2D.Kinematic;
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.anchor = collision.contacts[0].point;
            joint.connectedBody = collision.collider.GetComponent<Rigidbody2D>();
            joint.enableCollision = false;
            collision.transform.GetComponent<BoxScript>().changeNplayersRest();

            if (!photonView.IsMine || !collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
                collision.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);

            }
        }

        if (!photonView.IsMine)
        {

            if (collision.collider.CompareTag("Player") && collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                //Transfer PhotonView of Rigidbody to our local player
                Debug.Log("Cambiado due�o");
                if (gameObject.GetComponent<PhotonView>().Owner.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
                }


            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

            if (!photonView.Owner.IsMasterClient)
            {

                //    photonView.TransferOwnership(PhotonNetwork.MasterClient);

            }
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
