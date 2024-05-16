using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    float speed = 10.0f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Get the mouse position in world-space. This will be our seek target!
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;

        // 2. Determine force vector (desired velocity - current velocity)
        Vector3 currentVelocity = rb.velocity;
        Vector3 desiredVelocity = (mouse - transform.position).normalized * speed;
        Vector3 seekForce = desiredVelocity - currentVelocity;

        // 3. Apply force vector
        rb.AddForce(seekForce);
    }
}
