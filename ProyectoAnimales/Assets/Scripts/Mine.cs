using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Mine : MonoBehaviourPun
{
    Animator explosion;

    private void Awake()
    {
        explosion = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PhotonNetwork.IsMasterClient)
        {
            explosion.SetBool("Explotada", true);
            StartCoroutine(Explotar());
        }
    }

    private IEnumerator Explotar()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(gameObject);
    }

}
