using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class cambiarGravedad : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Tipo { 
    izquierda, derecha, arriba
    }
    public Tipo tipo;
    
    float tiempoAplicarFuerza=0.25f;
    float tiempo;


    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Gravedad")||!collision.gameObject.GetComponentInParent<PhotonView>().IsMine) return;
        collision.GetComponentInParent<Rigidbody2D>().gravityScale = 0;
         tiempo=Time.time;
        switch (tipo) {

            case Tipo.izquierda:                
                collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-29.43f, 0), ForceMode2D.Impulse);
                collision.gameObject.GetComponentInParent<PlayerController>().g = PlayerController.gra.izquierda;
                collision.gameObject.GetComponentInParent<PlayerController>().Flip();
                StartCoroutine(activarControles(collision.transform.parent.gameObject));
                break;
            case Tipo.derecha:
                collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(29.43f, 0), ForceMode2D.Impulse);
                collision.gameObject.GetComponentInParent<PlayerController>().g = PlayerController.gra.derecha;
                collision.gameObject.GetComponentInParent<PlayerController>().Flip();
                StartCoroutine(activarControles(collision.transform.parent.gameObject));
                break;
            case Tipo.arriba:
                collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 29.43f), ForceMode2D.Impulse);
                collision.gameObject.GetComponentInParent<PlayerController>().g = PlayerController.gra.arriba;
                collision.gameObject.GetComponentInParent<PlayerController>().Flip();
                StartCoroutine(activarControles(collision.transform.parent.gameObject));
                break;
        }
            
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Gravedad") || !collision.gameObject.GetComponentInParent<PhotonView>().IsMine) return;

        if (Time.time - tiempoAplicarFuerza >= tiempo) {
            tiempo = Time.time;
            switch (tipo)
            {
                case Tipo.izquierda:
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-29.43f, 0), ForceMode2D.Impulse);
                    break;
                case Tipo.derecha:
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(29.43f, 0), ForceMode2D.Impulse);
                    break;
                case Tipo.arriba:
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 29.43f), ForceMode2D.Impulse);
                    break;
            }
        }
       
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Gravedad") || !collision.gameObject.GetComponentInParent<PhotonView>().IsMine) return;
        if (collision.GetComponentInParent<PlayerController>().g.ToString() != tipo.ToString()) return;
        collision.GetComponentInParent<Rigidbody2D>().gravityScale = 1;
        collision.gameObject.GetComponentInParent<PlayerController>().g = PlayerController.gra.normal;
        collision.gameObject.GetComponentInParent<PlayerController>().Flip();
    }

    IEnumerator activarControles(GameObject collision) {
        collision.GetComponentInParent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        collision.GetComponentInParent<PlayerInput>().enabled = true;
    }
}
