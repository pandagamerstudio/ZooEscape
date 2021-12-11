using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkColisionBoxIA : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxScriptIA box;

    void Start()
    {
        box = GetComponentInParent<BoxScriptIA>();
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
