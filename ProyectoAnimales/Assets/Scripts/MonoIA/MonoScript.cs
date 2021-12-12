using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoScript : MonoBehaviour
{
    
    int rutina;
    float cronometro;
    public Animator anim;
    Quaternion angulo;
    float grado;
    float NPCSpeed = 1.5f;
    public GameObject target;

    public GameObject llave;
    public GameObject boton;
    public GameObject checkPoint1, checkPoint2;
    bool irIzq, irDer, huyendo = false;
    bool atrapado = false;
    Vector3 scaleIzq = new Vector3 (-1.0f, 1.0f, 1.0f);
    Vector3 scaleDer = new Vector3 (1.0f, 1.0f, 1.0f);

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("OsoPardo");
        //rotacionInicial = gameObject.transform.rotation;
    }

    void Update(){
        comportamientoMono();
        //gameObject.transform.rotation = rotacionInicial;
    }

    // Update is called once per frame
    public void comportamientoMono(){
        if (Vector3.Distance(transform.position, target.transform.position) > 5){
            
            cronometro += 1 * Time.deltaTime;
            if (cronometro >=4){
                rutina = Random.Range(0,2);
                cronometro = 0;
            }

            switch (rutina){
                case 0:
                    anim.SetBool("Walk", false);
                    break;
                case 1:
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
                        if (transform.position.x <= checkPoint1.transform.position.x){
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

                    break;
            }
        }
        else {
            Debug.Log("Deberia huir");

            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            anim.SetBool("Walk", false);
            huyendo = false;

            if (huyendo){
                lookPos = Vector3.Normalize(lookPos);
                transform.position = Vector3.MoveTowards(transform.position, lookPos, Time.deltaTime *4);
                huyendo = true;
                anim.SetBool("Walk", true);
            }
            
        }

        if (GameObject.Find("Llave") == null && !atrapado){
            Debug.Log("Pulsar boton");
            huyendo = false;
            PulsarBoton();
        }
    }

    public void PulsarBoton(){
        StartCoroutine(esperando());

        Vector3 direccionFinal = boton.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, direccionFinal, Time.deltaTime * 6);
        
        if (transform.position.x < boton.transform.position.x){
            transform.localScale = scaleDer;
        } else {
            transform.localScale = scaleIzq;
        }

        if (transform.position == boton.transform.position){
            anim.SetBool("Walk", false);
            atrapado = true;
            Debug.Log("Atrapado");
            StartCoroutine(liberarJugador());
        }
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
