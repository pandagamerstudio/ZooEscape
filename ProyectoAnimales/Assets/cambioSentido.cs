using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioSentido : MonoBehaviour
{
    public Quaternion sentido;
   public int orientacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("agente")) {
            // other.gameObject.GetComponent<movimiento>().inicio= sentido;
            Debug.Log(gameObject.name);
            Debug.Log(orientacion);

            other.gameObject.GetComponent<movimiento>().cambiarOrientacion(orientacion);
        
        }
    }
}
