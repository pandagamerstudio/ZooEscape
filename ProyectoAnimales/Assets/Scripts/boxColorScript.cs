using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class boxColorScript : MonoBehaviourPun
{
    private Rigidbody2D caja;
    public static int idJug;
    bool active;
    

    private void Awake()
    {
        caja = GetComponent<Rigidbody2D>();
        active = false;
    }

    public void changeActive(bool a)
    {
        active = a;
        if (active){
            caja.constraints = RigidbodyConstraints2D.None;
            caja.constraints = RigidbodyConstraints2D.FreezeRotation;
        }else{
             caja.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }

    public void setIdJug(int id){
        idJug = id;
    }

}
