using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectIA : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("AudioManager").GetComponent<AudioVolume>().playSfx("llave");
            
            Destroy(gameObject);
        }

    }
}
