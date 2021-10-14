using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMovManager : MonoBehaviour
{
    public GameObject plataforma;
    private PlatformMovement platMov;
    
    // Start is called before the first frame update
    void Start()
    {
        //platMov = plataforma.transform.GetChild(0).GetComponent<PlatformMovement>();
        platMov = FindObjectOfType<PlatformMovement>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(platMov.activate);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entra");
            platMov.activate = true;
        }
    }
}
