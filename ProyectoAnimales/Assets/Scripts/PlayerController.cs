using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPun
{

    //[HideInInspector]
    public int id;

    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    public Rigidbody2D rb2D;
    public CheckGround cg;
    public HingeJoint2D hj;

    public float doubleJumpSpeed = 2.5f;
    public float pushForce = 10f;

    public bool canDoubleJump = false;
    public bool attached = false;

    public Transform attachedTo;
    private GameObject disregard;
    
    public bool superJump = false;
    public float lowJumpMultiplier = 1f;
    public float fallMultiplier = 0.5f;

    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator anim;
    Vector2 ad;
    float Salto;
    float agarrar;

    public static PlayerController me;
   
    
    void Awake(){
        rb2D = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        cg = this.GetComponentInChildren<CheckGround>();
        hj = this.GetComponent<HingeJoint2D>();

        //InputSystem.EnableDevice(Keyboard.current);
        //InputSystem.DisableDevice();
    }

    public void Movimiento(InputAction.CallbackContext callback) {
        if (!photonView.IsMine)
            return;

        ad = callback.ReadValue<Vector2>();
        rb2D.velocity = new Vector2(runSpeed*ad.x, rb2D.velocity.y);

        if (attached){
            if (ad.x < 0)
                rb2D.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            else
                rb2D.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
/*
            if (ad.y > 0)

            else

            */
        }
    }

    public void salto(InputAction.CallbackContext callback)
    {
        if (!photonView.IsMine)
            return;

        if(attached)
            return;

        Salto = callback.ReadValue<float>();

        if (cg.isGrounded)
        {
            canDoubleJump = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
        else
        {
            if (Salto > 0f)
            {
                if (canDoubleJump)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                    canDoubleJump = false;
                    return;
                }
            }
        }

        if (superJump)
        {
            if (rb2D.velocity.y > 0 && Salto == 0)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, Physics2D.gravity.y);
            }
        }

    }
    /*
    public void Agarrar(InputAction.CallbackContext callback) {
        agarrar = callback.ReadValue<Float>();

        if (agarrar > 0 && attached){
            Detach();
        }
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
    
        //Debug.Log(cg.isGrounded);
        
        


       
    }

    [PunRPC]
    public void Initialize(Player player){
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;

        this.gameObject.layer = 9+id;
    }
}
