using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D myRb;

    private Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRb.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag.Equals("suelo"))
        {
            Destroy(gameObject);
        }
    }
}
