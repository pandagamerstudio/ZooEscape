using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observerResolutor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] suscriptores;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("resolutor")) {
            Debug.Log(gameObject.name);
            suscriptores[0].GetComponent<resolutor>().pruebaCompletada();
        }
    }

   
}
