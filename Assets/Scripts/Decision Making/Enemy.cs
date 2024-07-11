using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    public Transform[] waypoints;
    int waypoint = 0;

    const float moveSpeed = 10.0f;
    const float turnSpeed = 1080.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float rotation = Steering.RotateTowardsVelocity(rb, turnSpeed, Time.deltaTime);
        rb.MoveRotation(rotation);

        Vector3 steeringForce = Vector2.zero;
        steeringForce += Steering.Seek(rb, waypoints[waypoint].transform.position, moveSpeed);
        steeringForce += Steering.Avoid(rb, moveSpeed, 2.5f);
        rb.AddForce(steeringForce);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        waypoint++;
        waypoint %= waypoints.Length;
    }
}
