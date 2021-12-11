using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController3d : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 2.5f;
    Rigidbody rb;
    CheckGround cg;
    public SpriteRenderer sr;
    public Animator anim;

    Vector2 ad;
    public int scale;


    //Efectos sonido
    AudioVolume sonido;

    public enum gra
    {
        normal,
        izquierda,
        derecha,
        arriba
    }
    public gra g;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        cg = this.GetComponentInChildren<CheckGround>();
        scale = 1;

        sonido = GameObject.Find("AudioManager").GetComponent<AudioVolume>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        checkGravedad();
        if (ad.x != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);

        }
    }
    public void checkGravedad()
    {
        switch (g)
        {
            case gra.normal:
                rb.velocity = new Vector3(runSpeed * ad.x, rb.velocity.y, rb.velocity.z);
                break;
            case gra.izquierda:
                rb.velocity = new Vector3(rb.velocity.x, -runSpeed * ad.x, rb.velocity.z);
               
                break;
            case gra.derecha:
                rb.velocity = new Vector3(rb.velocity.x,runSpeed*ad.x, rb.velocity.z);
                break;
            case gra.arriba:
                rb.velocity = new Vector3(-runSpeed * ad.x, rb.velocity.y, rb.velocity.z);
                break;

        }
      //  rb.velocity = new Vector3(runSpeed * ad.x, rb.velocity.y, rb.velocity.z);
    }
        public void Movimiento(InputAction.CallbackContext callback)
    {




        ad = callback.ReadValue<Vector2>();

        Flip();

    }
    public void salto(InputAction.CallbackContext callback)
    {

        Saltar(callback.ReadValue<float>());
    }

    public void Flip()
    {
        switch (g)
        {
            case gra.normal:
                transform.eulerAngles = Vector3.zero;
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
            case gra.izquierda:
                transform.eulerAngles = new Vector3(0, 0, -90f);
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
            case gra.derecha:
                transform.eulerAngles = new Vector3(0, 0, 90f);
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
            case gra.arriba:
                transform.eulerAngles = new Vector3(0, 0, 180f);
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
        }




      /*  transform.eulerAngles = Vector3.zero;
        if (ad.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
        }
        else if (ad.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
        }*/


    }
    void Saltar(float s)
    {
        float jumpM = 1f;
        if (scale == 2)
            jumpM = 1.5f;

        Vector3 velocidad = Vector3.zero;
        Vector3 velocidadDoble = Vector3.zero;
        Vector3 velocidadSuper = Vector3.zero;

        switch (g)
        {
            case gra.normal:
                velocidad = new Vector3(rb.velocity.x, jumpSpeed * jumpM,rb.velocity.z);
                break;
            case gra.izquierda:
                velocidad = new Vector3(jumpSpeed * jumpM, rb.velocity.y, rb.velocity.z);
                break;
            case gra.derecha:
                velocidad = new Vector3(-jumpSpeed * jumpM, rb.velocity.y, rb.velocity.z);
                break;
            case gra.arriba:
                velocidad = new Vector3(rb.velocity.x, -jumpSpeed * jumpM, rb.velocity.z);
                break;
        }
                rb.velocity = velocidad;
    }

    public void OnSaltarButton()
    {


        Saltar(1);
    }

    public void OnMoveButton(float dir)
    {


        ad = new Vector2(dir, 0);
        Flip();
    }
}
