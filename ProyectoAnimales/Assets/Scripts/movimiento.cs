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

    public GameObject[] objetivos;
    public int objetivoActual;


    bool click;
    bool tengoLlave;
    bool colocarmeParaEmpujar;
    bool animCaja;
    Vector3 puntomascercano;
    bool cajacolocada;
    bool heTerminadoCajas;
    int botonActual;

    public GameObject llave;
    public GameObject caja;
    public GameObject jugador;
    public GameObject cajaImaginaria;//Esto se convertira en un array que correspondera con la posicionCaja
    public GameObject cajaMovible;
    public GameObject []posicionesApilarse;
    public GameObject[] posicionCaja;
    public GameObject[] indicador;
    public GameObject[] boton;
    public GameObject sueloBoton;

    public GameObject[] suscrpitores;
    

    bool mirarBotones;
    bool moverCajaIzq;

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
        heTerminadoCajas = false;
        mirarBotones = true;
        movePos = Vector3.zero;
        objetivoActual = 0;
        tengoLlave = false;
        moverCajaIzq = false;
        botonActual = 0;
    }

    public void actualizarObjetivo(GameObject g) {
        //if (boton[0] == null)
        //    return;
        if (g.name.Equals("Boton") || g.name.Equals("Boton2")) {
            Debug.Log("Es un boton");
            if (g.GetComponent<actualizarObjetivoAgente>().objetivo != objetivoActual) {
                return;
            }
        }

       
        objetivoActual++;
        for (int i = 0; i < suscrpitores.Length; i++) {
            suscrpitores[i].GetComponent<objetivoCompanero4>().escuchando(objetivoActual);
        }
        if (objetivos[objetivoActual].name.Equals("Boton2") && objetivoActual > 2)
        {
            botonActual++;
            Debug.Log(" botonActual++ ");
        }
        Debug.Log("Objetivo actual " + objetivos[objetivoActual].name);
    }

    public void desActualizarObjetivo(int miObjetivo) {
        if (boton != null && objetivoActual != miObjetivo+1)
            return;

        
        objetivoActual--;
        for (int i = 0; i < suscrpitores.Length; i++)
        {
            suscrpitores[i].GetComponent<objetivoCompanero4>().escuchando(objetivoActual);
        }
        Debug.Log("Objetivo actual " + objetivos[objetivoActual].name);
    }
    IEnumerator desactivarIndicador(int i)
    {
        yield return new WaitForSeconds(2.0f);
        indicador[i].SetActive(false);
    }
        void Update()
    {
        transform.rotation= inicio;
        cajaMovible.transform.rotation = rotInicialcaja;
    


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
            if (moverCajaIzq)
            {
                if (caja.transform.parent.transform.position.x > puntomascercano.x)
                {
                    caja.transform.parent.transform.position = new Vector3(caja.transform.parent.transform.position.x - 0.01f, caja.transform.parent.transform.position.y, caja.transform.parent.transform.position.z);
                }
                else
                {
                    animCaja = false;
                    heTerminadoCajas = true;//Ya no hay que hacer más en el puzzle con cajas

                }
            }
            else {
                if (caja.transform.parent.transform.position.x < puntomascercano.x)
                {
                    caja.transform.parent.transform.position = new Vector3(caja.transform.parent.transform.position.x + 0.01f, caja.transform.parent.transform.position.y, caja.transform.parent.transform.position.z);
                }
                else
                {
                    animCaja = false;
                    heTerminadoCajas = true;//Ya no hay que hacer más en el puzzle con cajas

                }
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

    public void clickDerecho(InputAction.CallbackContext callback)
    {


        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && callback.phase == InputActionPhase.Started)
        {
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
                Debug.Log("Hay un obstáculo que no me deja mover la caja al punto de interes ");
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
                    cajaImaginaria.SetActive(false);
                    Debug.Log("Esta caja no es la solución ");

                }
                else
                {
                    cajaImaginaria.SetActive(false);
                    //Codigo de mover la caja falta hacer que determine la dirección en la que lo debe mover
                    Debug.Log("Esta caja es la solución ");
                   
                    if (objetivos[objetivoActual].transform.position.x < caja.transform.parent.transform.position.x && !cajacolocada)
                    {
                        //Me coloco hasta la derecha de la caja y se activa el desplazar la caja hacia el punto pata llegar a la llave
                        moverCajaIzq = true;
                        cajacolocada = true;
                        navMeshA.SetDestination(caja.transform.parent.transform.position + new Vector3(2, 0, 0));
                        //  puntomascercano = path.corners[path.corners.Length - 1];
                        puntomascercano = cajaImaginaria.transform.position;
                          colocarmeParaEmpujar = true;
                    }
                    else
                    {
                        Debug.Log("Mover Dcha");
                        moverCajaIzq = false;
                        cajacolocada = true;
                        navMeshA.SetDestination(caja.transform.parent.transform.position - new Vector3(3.5f, 0, 0));
                        puntomascercano = cajaImaginaria.transform.position;
                        colocarmeParaEmpujar = true;

                    }

                }

            }
        }
    }

    public void mejorAccion() {
        if (!tengoLlave) {

            //1. MIRAMOS SI NO TENEMOS COGIDA LA LLAVE
            //2. SI NO LA TENEMOS COMPROBAMOS SI EXISTE CAMINO PARA LLEGAR A ELLA
            RaycastHit hit;
            Physics.Raycast(objetivos[objetivoActual].transform.position, -Vector3.up, out hit);
            
            NavMeshPath path = new NavMeshPath();
            navMeshA.CalculatePath(hit.point, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("No hay camino directo ");

                //3. ¿HAY CAJA EN LA ESCENA? ¿PODEMOS HACER COSAS CON ELLA(FALTA ESTO)?
                if (caja.activeInHierarchy&&!heTerminadoCajas)
                {
                    caja.GetComponent<NavMeshObstacle>().enabled = false;
                    StartCoroutine(movimientosCaja(path, hit));

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
                            Debug.Log("Este apilarse no lleva a la solucion");

                        }
                        else
                        {
                            //Si el jugador se coloca en este sitio habrá cámino
                            Debug.Log("Jugador muevete aqui "+i);
                            indicador[i].SetActive(true);
                            StartCoroutine(desactivarIndicador(i));
                            posicionesApilarse[i].SetActive(false);//Lo desactivamos
                            mirarBotones = false;
                            break;

                        }

                    }
                    

                }
                if (boton[0]!=null&&mirarBotones && boton[0].activeInHierarchy&&objetivoActual<=2) {
                    //5. ¿QUE PASARÍA SI PULSO EL PRIMER BOTÓN?
                  
                   
                    boton[botonActual].GetComponent<BotonScript>().activarElementos();//Active el suelo pero no el meshRenderer(PARA COMPROBAR SI NOS LLEVA AL OBJETIVO ACTUAL);

                    Debug.Log("esta acitvo el mesh del suelo? " + boton[botonActual].GetComponent<BotonScript>().estaActivadoElSuelo());
                    navMeshA.CalculatePath(hit.point, path);
                    if (path.status == NavMeshPathStatus.PathPartial)
                    {
                        Debug.Log("Este boton no lleva a la solución");
                            boton[botonActual].GetComponent<BotonScript>().desactivarElementos();//Lo quitamos porque si no se queda el navMesh activo y podria llegar a la solucion
                    }
                    else {
                        Debug.Log("Este boton  lleva a la solución");
                            boton[botonActual].GetComponent<BotonScript>().desactivarElementos();//Lo quitamos porque si no se queda el navMesh activo y podria llegar a la solucion
                            navMeshA.SetDestination(puntoMallaObjetivo(boton[botonActual]));//Obtenemos la posición del boton para ir a él

                        
                    }
                }


            }
            else {
                Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    movePos = hit.point;
                    navMeshA.SetDestination(hit.point);
                }
                else {
                    Debug.Log("No se que hacer");

                }

            }


            mirarBotones = true;
        }
    
    }
}
