using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{

    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;

    [Header("Components")]
    public Rigidbody2D rig;
    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator anim;
    
    public static PlayerController me;

    void Awake(){
        rig = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    void Update(){
        if (!photonView.IsMine)
            return;

        Move();
    }

    void Move(){
        float x = Input.GetAxis("Horizontal");

        if (x != 0)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);

        if (x > 0)
            transform.localScale = new Vector3(3,3,1);
        else if (x < 0)
            transform.localScale = new Vector3(-3,3,1);

        rig.velocity = new Vector2(x,0) * moveSpeed;
    }

    [PunRPC]
    public void Initialize(Player player){
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;
    }

    [PunRPC]
    public void TakeDamage(int damage){

    }
    
}
