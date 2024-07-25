using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    const float moveForce = 50.0f;
    const float maxSpeed = 10.0f;
    Vector2 direction = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        direction = direction.normalized;
    }

    void FixedUpdate()
    {
        // Apply force based on input direction and reset for next input
        rb.AddForce(direction * moveForce);
        direction = Vector2.zero;

        // Limit velocity
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
