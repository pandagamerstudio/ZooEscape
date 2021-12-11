using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerIA : MonoBehaviour
{
    public float runSpeed = 7f;
    public float jumpSpeed = 10f;
    Rigidbody2D rb;
    CheckGround cg;
    public SpriteRenderer sr;
    public Animator anim;

    Vector2 ad;
    //Efectos sonido
    AudioVolume sonido;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        cg = this.GetComponentInChildren<CheckGround>();

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
        rb.velocity = new Vector3(runSpeed * ad.x, rb.velocity.y);
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
        transform.eulerAngles = Vector3.zero;
        if (ad.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (ad.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    void Saltar(float s)
    {
        float jumpM = 1f;

        Vector3 velocidad = Vector3.zero;
        Vector3 velocidadDoble = Vector3.zero;
        Vector3 velocidadSuper = Vector3.zero;

        velocidad = new Vector3(rb.velocity.x, jumpSpeed * jumpM);
        velocidadSuper = new Vector3(rb.velocity.x, Physics2D.gravity.y);

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
