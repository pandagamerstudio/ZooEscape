using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{

    [HideInInspector]
    public int id;

    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    Rigidbody2D rb2D;

    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator anim;

    public static PlayerMove me;
    
    void Awake(){
        rb2D = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;
    
        Debug.Log(CheckGround.isGrounded);
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        if((Input.GetKey("w") || Input.GetKey("up")) && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
    }

    [PunRPC]
    public void Initialize(Player player){
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;
    }
}
