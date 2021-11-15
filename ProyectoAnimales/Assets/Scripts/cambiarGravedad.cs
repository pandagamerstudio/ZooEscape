using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class cambiarGravedad : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Tipo { 
    izquierda
    }
    public Tipo tipo;
    
    float tiempoAplicarFuerza=0.25f;
    float tiempo;


    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player")||!collision.gameObject.GetComponent<PhotonView>().IsMine) return;
        collision.GetComponent<Rigidbody2D>().gravityScale = 0;
         tiempo=Time.time;
        switch (tipo) {

            case Tipo.izquierda:
                
                 collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-29.43f, 0), ForceMode2D.Impulse);
                 collision.gameObject.GetComponent<PlayerController>().g = PlayerController.gra.izquierda;
                collision.gameObject.GetComponent<PlayerController>().Flip();
                StartCoroutine(activarControles(collision));
                break;
        }
            
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player") || !collision.gameObject.GetComponent<PhotonView>().IsMine) return;

        if (Time.time - tiempoAplicarFuerza >= tiempo) {
            tiempo = Time.time;
            switch (tipo)
            {
                case Tipo.izquierda:
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-29.43f, 0), ForceMode2D.Impulse);

                    break;
            }
        }
       
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player") || !collision.gameObject.GetComponent<PhotonView>().IsMine) return;
        collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        collision.gameObject.GetComponent<PlayerController>().g = PlayerController.gra.normal;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator activarControles(Collider2D collision) {
        collision.GetComponent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        collision.GetComponent<PlayerInput>().enabled = true;
    }
}
