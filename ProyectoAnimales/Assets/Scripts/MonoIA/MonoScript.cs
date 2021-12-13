using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoScript : MonoBehaviour
{
    
    int rutina;
    float cronometro;
    Animator anim;
    GameObject target;
    public GameObject llave;
    public GameObject boton;
    public GameObject checkPoint1, checkPoint2;
    bool irIzq, irDer, huyendo = false;
    bool atrapado = false;
    bool irBoton = false;
    Vector3 scaleIzq = new Vector3 (-10.0f, 10.0f, 1.0f);
    Vector3 scaleDer = new Vector3 (10.0f, 10.0f, 1.0f);

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("OsoPardo");
    }

    void Update(){
        comportamientoMono();
    }

    // Update is called once per frame
    public void comportamientoMono(){
        float distancia = transform.position.x - target.transform.position.x;
        //Debug.Log(distancia); 
        if (Mathf.Abs(distancia) > 7 && !huyendo){
            
            cronometro += 1 * Time.deltaTime;
            if (cronometro >=4){
                rutina = Random.Range(0,2);
                cronometro = 0;
            }
            if(!irBoton){
                switch (rutina){
                case 0:
                    Debug.Log("Estoy quieto");
                    quedarseParado();
                    break;
                case 1:
                    Debug.Log("Yendo a checkpoint");
                    irCheckpoints();
                    break;
                }
            }
            
        }
        else {
            Debug.Log("Deberia huir");
            huir();          
        }

        if (GameObject.Find("Llave") == null && !atrapado){
            Debug.Log("Pulsar boton");
            irBoton = true;
            huyendo = false;
            PulsarBoton();
        }
    }

    public void PulsarBoton(){
        StartCoroutine(esperando());

        Vector3 direccionFinal = boton.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, direccionFinal, Time.deltaTime * 7);
        
        if (transform.position.x < boton.transform.position.x){
            transform.localScale = scaleDer;
        } else {
            transform.localScale = scaleIzq;
        }

        if (transform.position == boton.transform.position){
            anim.SetBool("Walk", false);
            atrapado = true;
            Debug.Log("Atrapado");
            irBoton = false;
            StartCoroutine(liberarJugador());
        }
    }

    public void irCheckpoints(){
        float distancia1 = transform.position.x - checkPoint1.transform.position.x;
        float distancia2 = checkPoint2.transform.position.x - transform.position.x;
        Mathf.Abs(distancia1);
        Mathf.Abs(distancia2);
        Vector3 direccionCheckPoint1 = checkPoint1.transform.position;
        Vector3 direccionCheckPoint2 = checkPoint2.transform.position;

        if(irIzq) transform.localScale = scaleIzq;
        if(irDer) transform.localScale = scaleDer;

            if (!irDer)
            {
                irIzq = true;
                if (transform.position.x <= checkPoint1.transform.position.x)
                {
                    Debug.Log("He llegado al 1");
                    irIzq = false;
                    irDer = true;    
                }

            transform.position = Vector3.MoveTowards(transform.position, direccionCheckPoint1, Time.deltaTime * 4);
            anim.SetBool("Walk", true);
            }
            else if(!irIzq)
            {
                irDer = true;
                if(transform.position.x >= checkPoint2.transform.position.x)
                {
                    Debug.Log("He llegado al 2");
                    irDer = false;
                    irIzq = true;
                }
    
            transform.position = Vector3.MoveTowards(transform.position, direccionCheckPoint2, Time.deltaTime * 4);
            anim.SetBool("Walk", true);
            }
    }

    public void quedarseParado(){
        anim.SetBool("Walk", false);
    }

    public void huir(){
        Vector3 lookPos;
        anim.SetBool("Walk", false);
        huyendo = true;

        if(irIzq) transform.localScale = scaleIzq;
        if(irDer) transform.localScale = scaleDer;

        if (transform.position.x <= target.transform.position.x)
        {
            irIzq = true;
            irDer = false; 
            lookPos = new Vector3 (-1f,0f,0f); 
        } else {
            irIzq = false;
            irDer = true;  
            lookPos = transform.position + new Vector3(15.0f, 0f,0f);   
        }

        if (huyendo){
            transform.position = Vector3.MoveTowards(transform.position, lookPos, Time.deltaTime *4);
            //transform.position = Vector2.Lerp(transform.position, -target.transform.position, Time.deltaTime*2);
            anim.SetBool("Walk", true);
        }
        
        float distancia = transform.position.x - target.transform.position.x;

        if(Mathf.Abs(distancia) > 15) huyendo = false;

    }
    private IEnumerator liberarJugador()
    {
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator esperando(){
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("walk", true);
    }
}
