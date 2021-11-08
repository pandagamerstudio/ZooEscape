using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkColisionBox : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxScript box;

    void Start()
    {
        box = GetComponent<BoxScript>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //photonView.RPC("changeNplayersRest", RpcTarget.All);
            box.changeNplayersRest();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //photonView.RPC("changeNplayersSum", RpcTarget.All);
            box.changeNplayersSum();
        }
    }
}
