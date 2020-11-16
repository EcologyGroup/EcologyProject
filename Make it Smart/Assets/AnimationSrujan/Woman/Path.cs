using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    public float moveSpeed = 2f;
    private int waypointIndex = 0;
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(waypointIndex<=waypoints.Length-1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            if(transform.position==waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

        }
    }
}
