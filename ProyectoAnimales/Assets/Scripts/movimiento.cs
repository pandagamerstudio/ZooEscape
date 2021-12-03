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

    private void Awake()
    {
        navMeshA = GetComponent<NavMeshAgent>();
        inicio = transform.rotation;
        click = false;
        colocarmeParaEmpujar = false;
        animCaja = false;
        cajacolocada = false;
        movePos = Vector3.zero;
    }
    void Update()
    {
        transform.rotation= inicio;
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

    public void mejorAccion() {
        if (!tengoLlave) {

            //1. MIRAMOS SI NO TENEMOS COGIDA LA LLAVE
            //2. SI NO LA TENEMOS COMPROBAMOS SI EXISTE CAMINO PARA LLEGAR A ELLA
            RaycastHit hit;
            Physics.Raycast(llave.transform.position, -Vector3.up, out hit);
            Debug.DrawRay(llave.transform.position, -Vector3.up,Color.green,100);
            NavMeshPath path = new NavMeshPath();
            navMeshA.CalculatePath(hit.point, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("No hay camino");
                //Mover izquierda caja
                if (llave.transform.position.x < caja.transform.parent.transform.position.x&&!cajacolocada)
                {
                    //Me coloco hasta la derecha de la caja y se activa el desplazar la caja hacia el punto pata llegar a la llave
                    cajacolocada = true;
                    navMeshA.SetDestination(caja.transform.parent.transform.position + new Vector3(2, 0, 0));
                    puntomascercano = path.corners[path.corners.Length - 1];
                    colocarmeParaEmpujar = true;
                }
                else { 
                
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
