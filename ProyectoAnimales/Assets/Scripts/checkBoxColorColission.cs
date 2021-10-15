using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBoxColorColission : MonoBehaviour
{
    private boxColorScript box;

    void Start()
    {
        box = GetComponentInParent<boxColorScript>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().id == boxColorScript.idJug)
                box.changeActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().id == boxColorScript.idJug)
                box.changeActive(false);
        }
    }
}
