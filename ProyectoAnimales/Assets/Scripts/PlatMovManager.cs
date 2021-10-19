using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatMovManager : MonoBehaviour
{
    private GameObject plataforma;
    private PlatformMovement platMov;
    bool activada = false;
    float pulsar;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            plataforma.GetComponentInChildren<PlatformMovement>().activate = !plataforma.GetComponentInChildren<PlatformMovement>().activate;
        }
    }

    public void setPlataforma(GameObject plat){
        plataforma = plat;
    }

}
