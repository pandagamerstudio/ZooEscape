using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] public GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    public bool activate;
    
    // Start is called before the first frame update
    void Start()
    {
        activate = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(activate);
        if(activate == true)
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}
