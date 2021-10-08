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

    public float doubleJumpSpeed = 2.5f;
    public bool canDoubleJump = false;
    
    public bool superJump = false;
    public float lowJumpMultiplier = 1f;
    public float fallMultiplier = 0.5f;

    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator anim;

    public static PlayerController me;
    
    void Awake(){
        rb2D = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if ((Input.GetKey("w") || Input.GetKey("up")))
        {
            if (CheckGround.isGrounded)
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }
            else
            {
                if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")))
                {
                    if (canDoubleJump)
                    {
                        rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                        canDoubleJump = false;
                    }
                }
            }

        }
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

        if (superJump)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity = Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb2D.velocity.y > 0 && !Input.GetKey("w"))
            {
                rb2D.velocity = Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }

    [PunRPC]
    public void Initialize(Player player){
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;
    }
}
