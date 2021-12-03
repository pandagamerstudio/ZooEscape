using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class movimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public NavMeshAgent agent;
    private NavMeshAgent navMeshA;
   public  NavMeshAgent cajaAgent;
    // Update is called once per frame
    public Vector3 movePos;
    Quaternion inicio;




    bool click;
    bool tengoLlave;
    bool colocarmeParaEmpujar;
    bool animCaja;
    Vector3 puntomascercano;
    bool cajacolocada;

    public GameObject llave;
    public GameObject caja;
    public GameObject jugador;
    public GameObject cajaImaginaria;//Esto se convertira en un array que correspondera con la posicionCaja
    public GameObject cajaMovible;
    public GameObject []posicionesApilarse;
    public GameObject[] posicionCaja;
    public GameObject indicador;


    Quaternion rotInicialcaja;
    private void Awake()
    {
        navMeshA = GetComponent<NavMeshAgent>();
        
        inicio = transform.rotation;
        click = false;
        rotInicialcaja = cajaMovible.transform.rotation;
        colocarmeParaEmpujar = false;
        animCaja = false;
        cajacolocada = false;
        movePos = Vector3.zero;

    }
    IEnumerator desactivarIndicador()
    {
        yield return new WaitForSeconds(2.0f);
        indicador.SetActive(false);
    }
        void Update()
    {
        transform.rotation= inicio;
        cajaMovible.transform.rotation = rotInicialcaja;
        tengoLlave = false;


        if (!navMeshA.pathPending&& click)
        {
            if (navMeshA.remainingDistance <= navMeshA.stoppingDistance)
            {
                if (!navMeshA.hasPath || navMeshA.velocity.sqrMagnitude == 0f)
                {
                    click = false;
                    mejorAccion();

                }
            }
        }
        if (!navMeshA.pathPending && colocarmeParaEmpujar)
        {
            if (navMeshA.remainingDistance <= navMeshA.stoppingDistance)
            {
                if (!navMeshA.hasPath || navMeshA.velocity.sqrMagnitude == 0f)
                {
                    colocarmeParaEmpujar = false;
                    animCaja = true;

                }

            }
                   
        }


        if (animCaja)
        {
            if (caja.transform.parent.transform.position.x >= puntomascercano.x+2)
            {
                caja.transform.parent.transform.position=  new Vector3(caja.transform.parent.transform.position.x - 0.01f, caja.transform.parent.transform.position.y, caja.transform.parent.transform.position.z);
            }
            else
            {
                animCaja = false;
            }
        }

        }

   public void clicando(InputAction.CallbackContext callback) {

     
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
        if (Physics.Raycast(ray, out hit)&& callback.phase==InputActionPhase.Started)
        {
            click = true;
            // MOVE OUR AGENT
            movePos = hit.point;
            navMeshA.SetDestination(hit.point);
            

        }

    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    //Le pasamos un objetivo y nos devulve el punto del navmesh donde se encuentra
    public Vector3 puntoMallaObjetivo(GameObject objetivo) {
        RaycastHit hit;
        Physics.Raycast(objetivo.transform.position, -Vector3.up, out hit);
        return hit.point;
    }

    IEnumerator movimientosCaja(NavMeshPath path, RaycastHit hit) {

        for (int i = 0; i < posicionCaja.Length; i++)
        {
            //Primero miramos si hay algun obstaculo que no nos permita llevar la caja  a ese punto (JUGADOR POR EJEMPLO)
            yield return new WaitForSeconds(1);
            cajaMovible.SetActive(true);
            cajaAgent.CalculatePath(puntoMallaObjetivo(posicionCaja[i]), path);
            Debug.Log(path.status);
            if (path.status == NavMeshPathStatus.PathInvalid|| path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("Hay un obstáculo que no me deja mover la caja al punto de interes");
                cajaMovible.SetActive(false);
                caja.GetComponent<NavMeshObstacle>().enabled = true;

            }
            else
            {
                cajaMovible.SetActive(false);
                caja.GetComponent<NavMeshObstacle>().enabled = true;
              //  cajaAgent.SetDestination(puntoMallaObjetivo(posicionCaja[i]));//ESTO SE PODRA BORRAR
                //Activamos ese objeto imáginario para ver si es la solución
                cajaImaginaria.SetActive(true);
                navMeshA.CalculatePath(hit.point, path);
                if (path.status == NavMeshPathStatus.PathPartial)
                {
                    Debug.Log("Esta caja no es la solución");

                }
                else
                {
                    //Codigo de mover la caja
                    Debug.Log("Esta caja es la solución");

                }
                cajaImaginaria.SetActive(false);

            }
        }
    }

    public void mejorAccion() {
        if (!tengoLlave) {

            //1. MIRAMOS SI NO TENEMOS COGIDA LA LLAVE
            //2. SI NO LA TENEMOS COMPROBAMOS SI EXISTE CAMINO PARA LLEGAR A ELLA
            RaycastHit hit;
            Physics.Raycast(llave.transform.position, -Vector3.up, out hit);
            
            NavMeshPath path = new NavMeshPath();
            navMeshA.CalculatePath(hit.point, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("No hay camino directo");
                if (true) {
                    caja.GetComponent<NavMeshObstacle>().enabled = false;
                    StartCoroutine(movimientosCaja(path, hit));
                

                }
                //3. ¿HAY CAJA EN LA ESCENA? ¿PODEMOS HACER COSAS CON ELLA(FALTA ESTO)?
                else if (caja.activeInHierarchy)
                {




                    /*  //Mover izquierda caja
                      if (llave.transform.position.x < caja.transform.parent.transform.position.x && !cajacolocada)
                      {
                          //Me coloco hasta la derecha de la caja y se activa el desplazar la caja hacia el punto pata llegar a la llave
                          cajacolocada = true;
                          navMeshA.SetDestination(caja.transform.parent.transform.position + new Vector3(2, 0, 0));
                          puntomascercano = path.corners[path.corners.Length - 1];
                          colocarmeParaEmpujar = true;
                      }
                      else
                      {

                      }*/
                }
                //4.¿HAY JUGADOR EN LA ESCENA? ¿MIRAMOS SI MOVIENDOLO A ALGUNA POSICION PUEDO LLEGAR SALTANDO SOBRE ÉL ?
                else if (jugador.activeInHierarchy)
                {
                    for (int i = 0; i < posicionesApilarse.Length; i++)
                    {
                        posicionesApilarse[i].SetActive(true);//Es invisible pero sirve para ver si colocandolo ahi se crea un camino al objetivo
                        navMeshA.CalculatePath(hit.point, path);
                        if (path.status == NavMeshPathStatus.PathPartial)
                        { //No existe camino
                            posicionesApilarse[i].SetActive(false);//Lo desactivamos

                        }
                        else
                        {
                            //Si el jugador se coloca en este sitio habrá cámino
                            Debug.Log("Jugador muevete aqui");
                            indicador.SetActive(true);
                            StartCoroutine(desactivarIndicador());
                            posicionesApilarse[i].SetActive(false);//Lo desactivamos
                            break;

                        }

                    }

                }


            }
            else {
                Debug.Log(hit.point);
                movePos = hit.point;
                navMeshA.SetDestination(hit.point);
            }
            

            
        }
    
    }
}
