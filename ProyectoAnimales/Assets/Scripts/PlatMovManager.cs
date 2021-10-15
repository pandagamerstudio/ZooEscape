using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMovManager : MonoBehaviour
{
    private GameObject plataforma;
    private PlatformMovement platMov;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !plataforma.GetComponentInChildren<PlatformMovement>().activate)
        {
            Debug.Log("Entra");
            plataforma.GetComponentInChildren<PlatformMovement>().activate = true;
        }
    }

    public void setPlataforma(GameObject plat){
        Debug.Log("Plataforma asignada");
        plataforma = plat;
    }
}
