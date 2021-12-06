using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class resolutor : MonoBehaviour
{
    public GameObject jug1;
    public GameObject jug2;
    public GameObject[] posicionesSpawnJug1;
    public GameObject[] posicionesSpawnJug2;
    public int[] objQueEscucho;

    public NavMeshAgent navJug1;
    public NavMeshAgent navJug2;



    public GameObject []objActPuzle;
    int objActual;


    int esconderPandas;


    public void evento(int objetivoAct) { 
        

    }

    private void Awake()
    {
        objActual = 0;
        esconderPandas = 0;

        jug1.renderer.material.color.a = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 puntoMallaObjetivo(GameObject objetivo)
    {
        RaycastHit hit;
        Physics.Raycast(objetivo.transform.position, -Vector3.up, out hit);
        return hit.point;
    }
    //public void darSolucionAct(InputAction.CallbackContext callback)
    //{
    //    mostrarSolucion();
    //}

    public void mostrarSolucion() {
        jug1.SetActive(true);
        jug2.SetActive(true);

        jug1.transform.position = posicionesSpawnJug1[objActual].transform.position;
        jug2.transform.position = posicionesSpawnJug2[objActual].transform.position;

        if (objActPuzle[objActual].tag.Equals("Llave")) {
            
            NavMeshPath path = new NavMeshPath();
            Vector3 puntoMalla = puntoMallaObjetivo(objActPuzle[objActual]);
            navJug1.CalculatePath(puntoMalla, path);
            navJug1.SetDestination(puntoMalla);
        }


    }


}
