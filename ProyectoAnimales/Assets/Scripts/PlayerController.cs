using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPun
{

    [HideInInspector]
    public int id;

    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    Rigidbody2D rb2D;
    public CheckGround cg;

    public float doubleJumpSpeed = 2.5f;

    public bool canDoubleJump = false;
    
    public bool superJump = false;
    public float lowJumpMultiplier = 1f;
    public float fallMultiplier = 0.5f;

    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator anim;
    Vector2 ad;
    bool Salto;

    public static PlayerController me;
   
    
    void Awake(){
        rb2D = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        cg = this.GetComponentInChildren<CheckGround>();
    }

    public void Movimiento(InputAction.CallbackContext callback) {
        ad = callback.ReadValue<Vector2>();
        
    }
    public void salto(InputAction.CallbackContext callback)
    {
        Salto = callback.ReadValue<bool>();

    }


    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
    
        Debug.Log(cg.isGrounded);
        
        rb2D.velocity = new Vector2(runSpeed*ad.x, rb2D.velocity.y);
        
     

        if (Salto)
        {
            if (cg.isGrounded)
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed );
            }
            else
            {
                if (Salto)
                {
                    if (canDoubleJump)
                    {
                        rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                        canDoubleJump = false;
                        return;
                    }
                }
            }

        }

        if (superJump)
        {
            if (rb2D.velocity.y > 0 && !Salto)
            {
                rb2D.velocity = new Vector2 (rb2D.velocity.x,Physics2D.gravity.y);
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
