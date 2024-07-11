using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    public Transform[] waypoints;
    int waypoint = 0;

    Steering steering;
    const float seekSpeed = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        steering = GetComponent<Steering>();
    }

    void Update()
    {
        // Don't actually use the steering script since it seeks/flees/avoids.
        // We want to seek/flee/avoid, but based on our own enemy-specific logic!
        //Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouse.z = 0.0f;
        //steering.target = mouse;

        // TODO -- add avoidance force (summate forces)
        rb.AddForce(Patrol());
    }

    Vector2 Patrol()
    {
        Vector2 target = waypoints[waypoint].transform.position;
        Vector2 seekForce = Steering.Seek(rb, target, seekSpeed);
        return seekForce;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        waypoint++;
        waypoint %= waypoints.Length;
    }
}
