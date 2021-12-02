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



    public GameObject llave;
    private void Awake()
    {
        navMeshA = GetComponent<NavMeshAgent>();
        inicio = transform.rotation;
        click = false;
        movePos = Vector3.zero;
    }
    void Update()
    {
        //   navMeshA.destination = movePos.position;
        transform.rotation= inicio;
        tengoLlave = false;
        /*   Debug.Log(navMeshA.isStopped);
            if (navMeshA.destination.Equals(movePos) && click) {
                Debug.Log("path completo");
                click = false;
            }*/

        if (!navMeshA.pathPending&& click)
        {
            if (navMeshA.remainingDistance <= navMeshA.stoppingDistance)
            {
                if (!navMeshA.hasPath || navMeshA.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("path completo");
                    click = false;
                    mejorAccion();

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


    public void irHaciaObjetivo(Vector3 objetivo) { 
    
    }

    public void mejorAccion() {
        if (!tengoLlave) {

            // Miro la posición de la llave
        /*    Vector3 down = new Vector3(llave.transform.position.x, -llave.transform.position.y, llave.transform.position.z);
            Ray rayo = new Ray(llave.transform.position,new Vector3(1,-1,0));*/
            RaycastHit hit;
            if (Physics.Raycast(llave.transform.position,-Vector3.up, out hit)) {
                Debug.Log(hit.point);
                Debug.DrawRay(llave.transform.position, -Vector3.up,Color.green,10);
                movePos = hit.point;
                navMeshA.SetDestination(hit.point);

            }
        }
    
    }
}
