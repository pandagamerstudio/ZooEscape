using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cambiarGravedad3d : MonoBehaviour
{
    public enum Tipo
    {
        izquierda, derecha, arriba
    }
    public Tipo tipo;

    float tiempoAplicarFuerza = 0.25f;
    float tiempo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision) {
        if (!collision.gameObject.name.Equals("OsoPolar")){
            return;
        }
        collision.GetComponent<Rigidbody>().useGravity = false;
        tiempo = Time.time;
        switch (tipo)
        {

            case Tipo.izquierda:
                collision.GetComponent<Rigidbody>().AddForce(new Vector2(-2.0f, 0), ForceMode.Impulse);
                collision.gameObject.GetComponent<playerController3d>().g = playerController3d.gra.izquierda;
                collision.gameObject.GetComponent<playerController3d>().Flip();
                StartCoroutine(activarControles(collision));
                break;
            case Tipo.derecha:
                collision.GetComponent<Rigidbody>().AddForce(new Vector2(2.0f, 0), ForceMode.Impulse);
                collision.gameObject.GetComponent<playerController3d>().g = playerController3d.gra.derecha;
                collision.gameObject.GetComponent<playerController3d>().Flip();
                StartCoroutine(activarControles(collision));
                break;
            case Tipo.arriba:
                collision.GetComponent<Rigidbody>().AddForce(new Vector2(0, 2.0f), ForceMode.Impulse);
                collision.gameObject.GetComponent<playerController3d>().g = playerController3d.gra.arriba;
                collision.gameObject.GetComponent<playerController3d>().Flip();
                StartCoroutine(activarControles(collision));
                break;
        }

    }
    public void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.name.Equals("OsoPolar")) return;
       /* if (Time.time - tiempoAplicarFuerza >= tiempo)
        {
            tiempo = Time.time;
            switch (tipo)
            {
                case Tipo.izquierda:
                    other.GetComponent<Rigidbody>().AddForce(new Vector2(-29.43f, 0), ForceMode.Impulse);
                    break;
                case Tipo.derecha:
                    other.GetComponent<Rigidbody>().AddForce(new Vector2(29.43f, 0), ForceMode.Impulse);
                    break;
                case Tipo.arriba:
                    other.GetComponent<Rigidbody>().AddForce(new Vector2(0, 29.43f), ForceMode.Impulse);
                    break;
            }
        }*/

    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.name.Equals("OsoPolar") ) return;

        if (other.GetComponent<playerController3d>().g.ToString() != tipo.ToString()) return;
        other.GetComponent<Rigidbody>().useGravity = true;
        other.gameObject.GetComponent<playerController3d>().g = playerController3d.gra.normal;
        other.gameObject.GetComponent<playerController3d>().Flip();
    }
    IEnumerator activarControles(Collider collision)
    {
        collision.GetComponent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        collision.GetComponent<PlayerInput>().enabled = true;
    }
}
