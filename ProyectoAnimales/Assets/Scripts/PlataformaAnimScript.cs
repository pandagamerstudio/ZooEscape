using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAnimScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetBool("1", true);
    }
}
