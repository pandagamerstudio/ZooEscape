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
    float gravedad;
    bool activadaGravedad = false;
    bool top = false;
    Vector2 gravedadLados;
    public static PlayerController me;

    GameObject canvas;
    
   
    
    void Awake(){
        rb2D = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        cg = this.GetComponentInChildren<CheckGround>();
        hj = this.GetComponent<HingeJoint2D>();

        //GetComponent<PlayerInput>().SwitchCurrentControlScheme.Gravedad;
        //InputSystem.EnableDevice(Keyboard.current);
        //InputSystem.DisableDevice();
    }

    void Start(){
        var user = GetComponent<PlayerInput>().user;
        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
        if (SystemInfo.deviceType == DeviceType.Desktop){
            user.ActivateControlScheme("Keyboard&Mouse");
            //canvas.SetActive(true);
        } else if (SystemInfo.deviceType == DeviceType.Handheld){
            user.ActivateControlScheme("Movil");   
            canvas.SetActive(true);
        }
    }

    public void Movimiento(InputAction.CallbackContext callback) {
        if (!photonView.IsMine)
            return;

        ad = callback.ReadValue<Vector2>();
        rb2D.velocity = new Vector2(runSpeed*ad.x, rb2D.velocity.y);

        
        if (top == false){
            if (ad.x > 0){
            transform.localScale = new Vector3(1,1,1);
            } else if (ad.x < 0) {
            transform.localScale = new Vector3(-1,1,1);
            } 
        } else {
            if (ad.x < 0){
            transform.localScale = new Vector3(1,1,1);
            } else if (ad.x > 0) {
            transform.localScale = new Vector3(-1,1,1);
            }
        }

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
    
    public void Gravedad(InputAction.CallbackContext callback) {
        if (!photonView.IsMine)
            return;
        gravedad = callback.ReadValue<float>();

        if (gravedad > 0 && activadaGravedad == false){
            if (top == false){
                transform.eulerAngles = new Vector3(0,0,180f);
                Physics2D.gravity = new Vector2 (0,9.81f);
            } else {
                transform.eulerAngles = Vector3.zero;
                Physics2D.gravity = new Vector2 (0,-9.81f);
            }

            top = !top;
            StartCoroutine(activarGravedad());
        }
        
    }

    private IEnumerator activarGravedad()
    {
        activadaGravedad = true;
        yield return new WaitForSeconds(1f);
        activadaGravedad = false;
    }

    public void GravedadLados(InputAction.CallbackContext callback) {
        if (!photonView.IsMine)
            return;
        gravedadLados = callback.ReadValue<Vector2>();

        if (gravedadLados.x > 0){
            Physics2D.gravity = new Vector2 (9.81f, 0);
            transform.eulerAngles = new Vector3(0,0,90f);
        } else if (gravedadLados.x < 0){
            Physics2D.gravity = new Vector2 (-9.81f, 0);
            transform.eulerAngles = new Vector3(0,0,-90f);
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
