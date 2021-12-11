using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class movimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public NavMeshAgent agent;
    private NavMeshAgent navMeshA;
   public  NavMeshAgent cajaAgent;
    // Update is called once per frame
    public Vector3 movePos;
    public Quaternion inicio;

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
    public GameObject[] cajaImaginaria;//Esto se convertira en un array que correspondera con la posicionCaja
    public GameObject cajaMovible;
    public GameObject cajaBoton;
    public GameObject []posicionesApilarse;
    public GameObject[] posicionCaja;
    public GameObject[] indicador;
    public GameObject[] boton;
    public GameObject sueloBoton;

    public GameObject[] suscrpitores;
    

    bool mirarBotones;
    bool moverCajaIzq;
    bool moverCajaBoton;
    bool colocarmeParaEmpujarCajaBoton;
    Quaternion rotInicialcaja;


    public GameObject fondoPensamiento;
    public GameObject accionAndar;
    public GameObject irBoton;
    public GameObject apilar;
    public GameObject algoMeCorta;
    public GameObject empujar;

    public GameObject resolutor;

    int cajasporVer = 0;

    public int movimientosDeCajaPorNivel;

    public int []desviaciones;
    int desviacion =0;
    

    enum orientacion{
    normal,
    izq,
    arriba,
    dcha
    }
    int orientacionAct=0;
    orientacion sentido=orientacion.normal;

    public void cambiarOrientacion(int i) {

        StartCoroutine(cambiarOr(i));
    }
    IEnumerator cambiarOr(int i) {
        yield return new WaitForSeconds(0.1f);
        if (i == orientacionAct)
        {
            orientacionAct = 0;
            sentido = orientacion.normal;
            inicio = new Quaternion(0, 0, 0, 1);
            desviacion = 0;
        }
        else if (i == 1)
        {
            sentido = orientacion.izq;
            inicio = new Quaternion(100, 100, 0, 1);
            desviacion = desviaciones[i];
            orientacionAct = 1;
        }
        else if (i == 2)
        {
            sentido = orientacion.arriba;
            inicio = new Quaternion(50, 0, 0, 1);
            desviacion = 0;
            orientacionAct = 2;

        }
        else if (i == 3)
        {
            orientacionAct = 3;

            sentido = orientacion.dcha;
            inicio = new Quaternion(0, 0, 1, 1);
            desviacion = 0;

        }
    }
    private void Awake()
    {
        navMeshA = GetComponent<NavMeshAgent>();
        
        inicio = transform.rotation;
        click = false;
        if(cajaMovible!=null)
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
        moverCajaBoton = false;
        colocarmeParaEmpujarCajaBoton = false;
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

        if (resolutor != null) {
            resolutor.GetComponent<resolutor>().actualizarObjetivo(objetivoActual);
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

        if (resolutor != null)
        {
            resolutor.GetComponent<resolutor>().desactualizarObjetivo(objetivoActual);
        }
        objetivoActual--;
        for (int i = 0; i < suscrpitores.Length; i++)
        {
            suscrpitores[i].GetComponent<objetivoCompanero4>().escuchando(objetivoActual);
        }
        Debug.Log("Objetivo actual " + objetivos[objetivoActual].name);
    }
    IEnumerator mostrarAccion(GameObject accion) {
        fondoPensamiento.SetActive(true);
        accion.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        fondoPensamiento.SetActive(false);
        accion.SetActive(false);

    }
    IEnumerator desactivarIndicador(int i)
    {
        yield return new WaitForSeconds(2.0f);
        indicador[i].SetActive(false);
    }
        void Update()
    {
        transform.rotation= inicio;
        if(cajaMovible!=null)
        cajaMovible.transform.rotation = rotInicialcaja;
    


        if (!navMeshA.pathPending&& click)
        {
            
            if (navMeshA.remainingDistance - desviacion <= navMeshA.stoppingDistance)
            {
                if (!navMeshA.hasPath || navMeshA.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("click hecho");
                    click = false;
                    mejorAccion();

                }
            }
        }
        if (!navMeshA.pathPending && colocarmeParaEmpujar)
        {
         //   Debug.Log(navMeshA.remainingDistance + " " + navMeshA.stoppingDistance);

            if (navMeshA.remainingDistance <= navMeshA.stoppingDistance)
            {
         //       Debug.Log(navMeshA.remainingDistance + " ,"+ navMeshA.stoppingDistance);
         //       Debug.Log("2");

                if (!navMeshA.hasPath || navMeshA.velocity.sqrMagnitude == 0f)
                {
        //            Debug.Log("3");

                    colocarmeParaEmpujar = false;
                    animCaja = true;

                }

            }
                   
        }
        if (!navMeshA.pathPending && colocarmeParaEmpujarCajaBoton)
        {
            if (navMeshA.remainingDistance <= navMeshA.stoppingDistance)
            {
                if (!navMeshA.hasPath || navMeshA.velocity.sqrMagnitude == 0f)
                {
                    colocarmeParaEmpujarCajaBoton = false;
                    moverCajaBoton = true;

                }

            }

        }

        if (animCaja)
        {
            caja.GetComponent<NavMeshObstacle>().enabled = false;
            if (moverCajaIzq)
            {
                if (caja.transform.parent.transform.position.x > puntomascercano.x)
                {

                    caja.transform.parent.transform.position = new Vector3(caja.transform.parent.transform.position.x - 0.01f, caja.transform.parent.transform.position.y, caja.transform.parent.transform.position.z);
                    navMeshA.SetDestination(caja.transform.position);
                }
                else
                {
                    movimientosDeCajaPorNivel--;
                    cajaMovible.transform.position = caja.transform.position;
                    if (movimientosDeCajaPorNivel <= 0)
                    {
                        heTerminadoCajas = true;//Ya no hay que hacer más en el puzzle con cajas
                        
                    }
                    animCaja = false;
                    caja.GetComponent<NavMeshObstacle>().enabled = true;
                    navMeshA.ResetPath();

                }
            }
            else {
                if (caja.transform.parent.transform.position.x < puntomascercano.x)
                {
                    caja.transform.parent.transform.position = new Vector3(caja.transform.parent.transform.position.x + 0.01f, caja.transform.parent.transform.position.y, caja.transform.parent.transform.position.z);
                    Debug.Log("pollalala");
                    navMeshA.SetDestination(caja.transform.position);
                }
                else
                {
                    movimientosDeCajaPorNivel--;
                    if (movimientosDeCajaPorNivel <= 0)
                    {
                        heTerminadoCajas = true;//Ya no hay que hacer más en el puzzle con cajas
                    }
                    animCaja = false;
                    caja.GetComponent<NavMeshObstacle>().enabled = true;
                    navMeshA.ResetPath();


                }
            }
            
        }

        if (moverCajaBoton) {
            cajaBoton.transform.position = Vector3.MoveTowards(cajaBoton.transform.position,posicionCaja[1].transform.position,Time.deltaTime*5);
            if (Vector3.Distance(cajaBoton.transform.position, posicionCaja[1].transform.position) < 0.001f) {
                moverCajaBoton = false;
                movimientosDeCajaPorNivel--;
                if (movimientosDeCajaPorNivel <= 0)
                {
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
            if (sentido == orientacion.izq)
            {
                gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0, 0.1f);
            }
            navMeshA.SetDestination(hit.point);

            click = true;
            // MOVE OUR AGENT
            Debug.Log(hit.point);
            movePos = hit.point;
            
            
            

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

    IEnumerator movimientosEsp(NavMeshPath path) {
        yield return new WaitForSeconds(1);
        cajaMovible.SetActive(true);
        cajaAgent.CalculatePath(puntoMallaObjetivo(posicionCaja[0]), path);
        Debug.Log(path.status);
        if (path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Hay un obstáculo que no me deja mover la caja al punto de interes ");
            cajaMovible.SetActive(false);
            caja.GetComponent<NavMeshObstacle>().enabled = true;

        }
        else {
            cajaMovible.SetActive(false);
            caja.GetComponent<NavMeshObstacle>().enabled = true;
            if (objetivos[objetivoActual].transform.position.x < caja.transform.parent.transform.position.x && !cajacolocada)
            {
                //Me coloco hasta la derecha de la caja y se activa el desplazar la caja hacia el punto pata llegar a la llave
                moverCajaIzq = true;
                cajacolocada = true;
                navMeshA.SetDestination(caja.transform.parent.transform.position + new Vector3(2, 0, 0));
                //  puntomascercano = path.corners[path.corners.Length - 1];
                puntomascercano = cajaImaginaria[0].transform.position;
                colocarmeParaEmpujar = true;
                StartCoroutine(mostrarAccion(empujar));
            }
            else
            {
                Debug.Log("Mover Dcha");
                moverCajaIzq = false;
                cajacolocada = true;
                navMeshA.SetDestination(caja.transform.parent.transform.position - new Vector3(3.5f, 0, 0));
                puntomascercano = cajaImaginaria[0].transform.position;
                colocarmeParaEmpujar = true;
                StartCoroutine(mostrarAccion(empujar));
            }
            actualizarObjetivo(gameObject);
        }
    }
    IEnumerator movimientosCaja(NavMeshPath path, RaycastHit hit) {

        for (int i = cajasporVer; i < posicionCaja.Length; i++)
        {
            //Primero miramos si hay algun obstaculo que no nos permita llevar la caja  a ese punto (JUGADOR POR EJEMPLO)
            yield return new WaitForSeconds(1);

            if (posicionCaja[i].name.Equals("cajaBotonPosFinal")) {
                caja.GetComponent<NavMeshObstacle>().enabled = true;
                colocarmeParaEmpujarCajaBoton = true;
                navMeshA.SetDestination(GameObject.Find("cajaBotonPosFinal").transform.position
 + new Vector3(3.5f, 0, 0));
                Debug.Log("Caja boton "+i);

                break;
            }

            cajaMovible.SetActive(true);
            cajaAgent.CalculatePath(puntoMallaObjetivo(posicionCaja[i]), path);
            Debug.Log(path.status);
            if (path.status == NavMeshPathStatus.PathInvalid|| path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("Hay un obstáculo que no me deja mover la caja al punto de interes ");
                cajaMovible.SetActive(false);
                caja.GetComponent<NavMeshObstacle>().enabled = true;
                StartCoroutine(mostrarAccion(algoMeCorta));

            }
            else
            {
                cajaMovible.SetActive(false);
                caja.GetComponent<NavMeshObstacle>().enabled = true;
              //  cajaAgent.SetDestination(puntoMallaObjetivo(posicionCaja[i]));//ESTO SE PODRA BORRAR
                //Activamos ese objeto imáginario para ver si es la solución
                cajaImaginaria[i].SetActive(true);
                yield return new WaitForSeconds(0.05f);

                navMeshA.CalculatePath(hit.point, path);
                if (path.status == NavMeshPathStatus.PathPartial)
                {
                    cajaImaginaria[i].SetActive(false);
                    Debug.Log("Esta caja no es la solución ");

                }
                else
                {
                    cajaImaginaria[i].SetActive(false);
                    //Codigo de mover la caja falta hacer que determin
                    //e la dirección en la que lo debe mover
                    Debug.Log("Esta caja es la solución ");
                    cajasporVer++;
                    if (objetivos[objetivoActual].transform.position.x < caja.transform.parent.transform.position.x && !cajacolocada)
                    {
                        Debug.Log("Mover Izq");

                        Debug.Log(objetivos[objetivoActual].transform.position.x +" "+ caja.transform.parent.transform.position.x);

                        //Me coloco hasta la derecha de la caja y se activa el desplazar la caja hacia el punto pata llegar a la llave
                        moverCajaIzq = true;
                        cajacolocada = true;
                        Debug.Log(caja.transform.parent.transform.position + new Vector3(2, 0, 0));

                        if (SceneManager.GetActiveScene().name.Equals("comportamientosNivel3"))
                        {
                            navMeshA.SetDestination(caja.transform.parent.transform.position + new Vector3(3.0f, 0, 0));
                        }
                        else {
                            navMeshA.SetDestination(caja.transform.parent.transform.position + new Vector3(2, 0, 0));

                        }

                        //  puntomascercano = path.corners[path.corners.Length - 1];
                        puntomascercano = cajaImaginaria[i].transform.position;
                        colocarmeParaEmpujar = true;
                        StartCoroutine(mostrarAccion(empujar));
                    }
                    else
                    {
                        Debug.Log("Mover Dcha");

                        Debug.Log(objetivos[objetivoActual].transform.position.x +" "+ caja.transform.parent.transform.position.x);
                        moverCajaIzq = false;
                        cajacolocada = true;
                        navMeshA.SetDestination(caja.transform.parent.transform.position - new Vector3(3.5f, 0, 0));
                        puntomascercano = cajaImaginaria[i].transform.position;
                        colocarmeParaEmpujar = true;
                        StartCoroutine(mostrarAccion(empujar));
                    }
                    break;
                }

            }
        }
    }

    public void mejorAccion() {
        if (!tengoLlave) {

            //1. MIRAMOS SI NO TENEMOS COGIDA LA LLAVE
            //2. SI NO LA TENEMOS COMPROBAMOS SI EXISTE CAMINO PARA LLEGAR A ELLA
            RaycastHit hit;
            Physics.Raycast(objetivos[objetivoActual].transform.position, -Vector3.up, out hit,2);
            
            NavMeshPath path = new NavMeshPath();
            navMeshA.CalculatePath(hit.point, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("No hay camino directo ");

                //3. ¿HAY CAJA EN LA ESCENA? ¿PODEMOS HACER COSAS CON ELLA(FALTA ESTO)?
                if (caja!=null&&caja.activeInHierarchy && !heTerminadoCajas)
                {
                    caja.GetComponent<NavMeshObstacle>().enabled = false;
                    StartCoroutine(movimientosCaja(path, hit));

                }
                //4.¿HAY JUGADOR EN LA ESCENA? ¿MIRAMOS SI MOVIENDOLO A ALGUNA POSICION PUEDO LLEGAR SALTANDO SOBRE ÉL ?
                else if (jugador!=null&&jugador.activeInHierarchy)
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
                            Debug.Log("Jugador muevete aqui " + i);
                            indicador[i].SetActive(true);
                            StartCoroutine(desactivarIndicador(i));
                            posicionesApilarse[i].SetActive(false);//Lo desactivamos
                            mirarBotones = false;
                            StartCoroutine(mostrarAccion(apilar));
                            break;

                        }

                    }

                }
                if (boton.Length != 0&&objetivos[objetivoActual].tag.Equals("boton") && mirarBotones && boton[0].activeInHierarchy && objetivoActual <= 2) {
                    //5. ¿QUE PASARÍA SI PULSO EL PRIMER BOTÓN?


                    boton[botonActual].GetComponent<botonScript3d>().activarElementos();//Active el suelo pero no el meshRenderer(PARA COMPROBAR SI NOS LLEVA AL OBJETIVO ACTUAL);

                    navMeshA.CalculatePath(hit.point, path);
                    if (path.status == NavMeshPathStatus.PathPartial)
                    {
                        Debug.Log("Este boton no lleva a la solución");
                        boton[botonActual].GetComponent<botonScript3d>().desactivarElementos();//Lo quitamos porque si no se queda el navMesh activo y podria llegar a la solucion
                    }
                    else {
                        Debug.Log("Este boton  lleva a la solución");
                        boton[botonActual].GetComponent<botonScript3d>().desactivarElementos();//Lo quitamos porque si no se queda el navMesh activo y podria llegar a la solucion
                        navMeshA.SetDestination(puntoMallaObjetivo(boton[botonActual]));//Obtenemos la posición del boton para ir a él
                        StartCoroutine(mostrarAccion(irBoton));


                    }
                }


            }
            else if (hit.collider == null && objetivos[objetivoActual].name.Equals("cune4")){
                
                    caja.GetComponent<NavMeshObstacle>().enabled = false;
                    StartCoroutine(movimientosEsp(path));

                
            }
            else {
                Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    movePos = hit.point;
                    navMeshA.SetDestination(hit.point);
                    int accion=objetivos[objetivoActual].layer;
                    if (accion == 17)
                    {
                        StartCoroutine(mostrarAccion(irBoton));

                    }
                    else if (accion == 6) {
                        StartCoroutine(mostrarAccion(empujar));
                    } else {
                        StartCoroutine(mostrarAccion(accionAndar));

                    }

                    Debug.Log("Voy al objetivo");

                }
                else {
                    Debug.Log("No se que hacer");

                }

            }


            mirarBotones = true;
        }
    
    }
}
