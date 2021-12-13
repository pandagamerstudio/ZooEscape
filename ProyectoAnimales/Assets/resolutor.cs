using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class resolutor : MonoBehaviour
{
    public GameObject jug1;
    public GameObject jug2;
    public GameObject jugNoNav;
    public GameObject jugNoNav2;
    public GameObject osoApilar;

    public GameObject cajaAnim;
    public GameObject[] posicionesSpawnJug1;
    public GameObject[] posicionesSpawnJug2;
    public GameObject[] posicionesSpawnCaja;
    public GameObject spawnNoNavAgent;

    public List<int> objQueEscucho;

    public NavMeshAgent navJug1;
    public NavMeshAgent navJug2;



    public GameObject []objActPuzle;


   public int objActual;

    Quaternion rotJug1;
    Quaternion rotJug2;


   public GameObject []accionesJug1;
   public GameObject []accionesJug2;
    public GameObject[] accionesCaja;


    bool moverCaja;
    bool moverNoNavAPunto;

  public  bool cooldown;
  public  bool cooldown2;
    public int coolDownsCompletados = 0;
    int coolDownsNecesarios = 0;

    //Los tengo harcodeados
    public GameObject[] spawnsNoNavAgent1;
    //Los tengo en el editor
    public GameObject[] spawnsNoNavAgent2;
    public GameObject[] puntosObjetivoNoNavAgent2;

    public BoxCollider2D casoRaro;
    public void evento(int objetivoAct) { 
        

    }

    private void Awake()
    {
        objActual = 0;

        jug1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        jug2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        jugNoNav.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

        if(osoApilar!=null)
            osoApilar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        if (jugNoNav2!=null)
        jugNoNav2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

        if(cajaAnim!=null)
        cajaAnim.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
        rotJug1 = jug1.transform.rotation;
        rotJug2 = jug2.transform.rotation;
        moverCaja = false;
        cooldown = false;
        cooldown2 = false;
        moverNoNavAPunto = false;


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        jug1.transform.rotation= rotJug1;
        jug2.transform.rotation= rotJug2;

        if (moverCaja) {
            cajaAnim.transform.position = Vector3.MoveTowards(cajaAnim.transform.position, accionesCaja[objActual].transform.position, Time.deltaTime * 5);
            jugNoNav.transform.position = Vector3.MoveTowards(jugNoNav.transform.position, cajaAnim.transform.position, Time.deltaTime * 5);
             if (jugNoNav2!=null&&jugNoNav2.active == true) {
            jugNoNav2.transform.position= Vector3.MoveTowards(jugNoNav2.transform.position, puntosObjetivoNoNavAgent2[objActual].transform.position, Time.deltaTime * 5);
          
               
            }
            if (Vector3.Distance(cajaAnim.transform.position, accionesCaja[objActual].transform.position) < 0.001f){
                moverCaja = false;
            }
        }

      
            if (!navJug1.pathPending && cooldown)
            {
                if (navJug1.remainingDistance <= navJug1.stoppingDistance)
                {
                    if (!navJug1.hasPath || navJug1.velocity.sqrMagnitude == 0f)
                    {

                        Debug.Log("Holaaa");
                    cooldown = false;
                    if (osoApilar != null)
                        osoApilar.SetActive(false);

                    coolDownsCompletados++;
                        if (coolDownsCompletados == coolDownsNecesarios)
                        {
                            coolDownsCompletados = 0;
                            coolDownsNecesarios = 0;
                        }

                    }
                }
            }
        
        
        if (cooldown2&&!navJug2.pathPending )
        {
            if (navJug2.remainingDistance <= navJug2.stoppingDistance)
            {
                if (!navJug2.hasPath || navJug2.velocity.sqrMagnitude == 0f)
                {
                    coolDownsCompletados++;
                    cooldown2 = false;

                    if (coolDownsCompletados == coolDownsNecesarios)
                    {
                        coolDownsCompletados = 0;
                        coolDownsNecesarios = 0;

                    }

                }
            }
        }

        if (moverNoNavAPunto) {
            jugNoNav.transform.position = Vector3.MoveTowards(jugNoNav.transform.position, objActPuzle[objActual].transform.position, Time.deltaTime * 5);
            if (Vector3.Distance(jugNoNav.transform.position, objActPuzle[objActual].transform.position) < 0.001f)
            {
                moverNoNavAPunto = false;
                jugNoNav.SetActive(true);
            }
            }

    }
    public Vector3 puntoMallaObjetivo(GameObject objetivo)
    {
        RaycastHit hit;
        Physics.Raycast(objetivo.transform.position, -Vector3.up, out hit);
        return hit.point;
    }
    public void darSolucionAct(InputAction.CallbackContext callback)
    {
        mostrarSolucion();
    }

    public void mostrarSolucion() {
        if (cooldown)
        {
            Debug.Log("Esta en cooldown");
            return;
        }
        
        if(casoRaro!=null)
        casoRaro.enabled = false;

        jug1.SetActive(true);
        jug1.GetComponent<SpriteRenderer>().enabled = true;
        jug2.SetActive(true);
        jug2.GetComponent<SpriteRenderer>().enabled = true;
        //   jug2.SetActive(true);

        //Si es una caja
        if (objActPuzle[objActual].layer == 6)
        {
            Debug.Log("casosos cajaj");
            jug1.SetActive(false);
            jug2.SetActive(false);

            jugNoNav.SetActive(true);
            cajaAnim.SetActive(true);

            moverCaja = true;
            jugNoNav.transform.position = new Vector3(cajaAnim.transform.position.x + cajaAnim.transform.localScale.x - 1 / 2, jugNoNav.transform.position.y, jugNoNav.transform.position.z);
            cajaAnim.transform.position = posicionesSpawnCaja[objActual].transform.position;


        }//Boton
        else if (objActPuzle[objActual].layer == 17)
        {
            //Poner su spawn point
            jug1.SetActive(false);
            jug2.SetActive(false);
            jugNoNav.SetActive(true);
            jugNoNav.transform.position = spawnNoNavAgent.transform.position;
            moverNoNavAPunto = true;
        }
        //MOver caja + movimiento del otro
        else if (objActPuzle[objActual].layer == 19)
        {
            if(casoRaro!=null)
            casoRaro.enabled = true;

            jug1.SetActive(false);
            jug2.SetActive(false);

            jugNoNav.SetActive(true);
            jugNoNav.transform.position = spawnsNoNavAgent1[objActual].transform.position;

            if (spawnsNoNavAgent2[objActual] != null)
            {
                jugNoNav2.SetActive(true);
                jugNoNav2.transform.position = spawnsNoNavAgent2[objActual].transform.position;
            }


            cajaAnim.SetActive(true);
            cajaAnim.transform.position = posicionesSpawnCaja[objActual].transform.position;

            moverCaja = true;


            //    jugNoNav.transform.position = new Vector3(cajaAnim.transform.position.x + cajaAnim.transform.localScale.x - 1 / 2, jugNoNav.transform.position.y, jugNoNav.transform.position.z);



        }
        else if (objActPuzle[objActual].layer == 20) {

            osoApilar.SetActive(true);
            jug2.SetActive(false);

            NavMeshPath path = new NavMeshPath();
            Vector3 puntoMalla = puntoMallaObjetivo(accionesJug1[objActual]);
            navJug1.CalculatePath(puntoMalla, path);
            navJug1.SetDestination(puntoMalla);

        }
        else
        {
            //Apilarse
            NavMeshPath path = new NavMeshPath();
            if (accionesJug1[objActual] != null)
            {

                Vector3 puntoMalla = puntoMallaObjetivo(accionesJug1[objActual]);
                navJug1.CalculatePath(puntoMalla, path);
                navJug1.SetDestination(puntoMalla);
            }


            //Debug.Log(puntoMalla);




            Vector3 puntoMalla2 = puntoMallaObjetivo(accionesJug2[objActual]);
            navJug2.CalculatePath(puntoMalla2, path);
            navJug2.SetDestination(puntoMalla2);

        }

    }


    public void pruebaCompletada() {

        StartCoroutine(quitarPersonajes());
    }

    IEnumerator quitarPersonajes()
    {



        yield return new WaitForSeconds(1);

        //Debug.Log(posicionesSpawnJug1[objActual].transform.position);
        // navJug1.Warp(posicionesSpawnJug1[objActual].transform.position);

        //  navJug2.isStopped = true;
        //  navJug2.ResetPath();
        if (osoApilar != null)
            osoApilar.SetActive(false);

        if (jug1.active) {
            jug1.GetComponent<SpriteRenderer>().enabled = false;
            navJug1.SetDestination(puntoMallaObjetivo(posicionesSpawnJug1[objActual]));
            cooldown = true;
            coolDownsNecesarios++;
        }
           
        if (jug2.active) {
            jug2.GetComponent<SpriteRenderer>().enabled = false;
            navJug2.SetDestination(puntoMallaObjetivo(posicionesSpawnJug2[objActual]));
            cooldown2 = true;
            coolDownsNecesarios++;
        }


        //jug1.transform.position = posicionesSpawnJug1[objActual].transform.position;
        //jug2.transform.position = posicionesSpawnJug2[objActual].transform.position;

        //jug1.SetActive(false);
        //jug2.SetActive(false);

        jugNoNav.SetActive(false);
        if (jugNoNav2 != null) {
            jugNoNav2.SetActive(false);
        }
        if(cajaAnim!=null)
        cajaAnim.SetActive(false);

    }

    public void actualizarObjetivo(int objCompletado) {
        if (objQueEscucho.Contains(objCompletado)) {
            objActual++;
        }
    }

    public void desactualizarObjetivo(int objAct) {
        if (objQueEscucho.Contains(objAct-1))
        {
            objActual--;
        }
    }

}
