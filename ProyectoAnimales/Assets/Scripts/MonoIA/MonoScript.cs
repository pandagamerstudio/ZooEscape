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
    public Quaternion rotacionInicial;
    
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
                    grado = Random.Range(0,360);
                    angulo = Quaternion.Euler(0, grado,0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward*1* Time.deltaTime);
                    anim.SetBool("Walk", true);
                    break;
            }
        }
        else {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            anim.SetBool("Walk", false);

            //Hacer que huya
            Debug.Log("Deberia huir");
            Vector3 direction = transform.position - target.transform.position;
            direction.y = 0;
            direction = Vector3.Normalize(direction);
            transform.rotation = Quaternion.Euler(direction);
            transform.Translate(transform.forward * NPCSpeed);
        }

        if (GameObject.Find("Llave") == null){
            Debug.Log("Pulsar boton");
        }
    }
}
