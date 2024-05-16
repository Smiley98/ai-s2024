using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Steering
{
    LINE,
    SEEK
}

public class Seek : MonoBehaviour
{
    public Steering steering;


    float speed = 10.0f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Switch behaviour every time space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (steering == Steering.SEEK)
            {
                steering = Steering.LINE;

                // Must reset rigid-body velocity since line-seek is doesn't use rb velocity
                rb.velocity = Vector3.zero;
            }
            else if (steering == Steering.LINE)
            {
                steering = Steering.SEEK;
            }
        }

        if (steering == Steering.LINE)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, mouse, speed * Time.deltaTime);
        }
        else if (steering == Steering.SEEK)
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
}
