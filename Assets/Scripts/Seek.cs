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
        //transform.Rotate(0.0f, 0.0f, 30.0f);
        //transform.Rotate(0.0f, 0.0f, -45.0f);
        // Relative rotation example ^
    }

    // How Vector3.MoveTowards works internally
    Vector3 SeekLine(Vector3 A, Vector3 B, float speed)
    {
        // AB = B - A
        Vector3 direction = (B - A).normalized;
        float distance = speed * Time.deltaTime;
        return direction * distance;
        // TODO -- clamp distance
    }

    // Homework task 1: turn this into a static method that can be applied to any
    // GameObject which has all the data needed to seek (rb & speed)
    Vector3 SeekCurve(Rigidbody2D seeker, Vector2 target, float speed)
    {
        Vector2 cv = seeker.velocity;
        Vector2 dv = (target - seeker.position).normalized * speed;
        return dv - cv;
    }

    void Update()
    {
        // Use the mouse as our target
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;

        // Manual calculation
        //Vector3 currentVelocity = rb.velocity;
        //Vector3 desiredVelocity = (mouse - transform.position).normalized * speed;
        //Vector3 seekForce = desiredVelocity - currentVelocity;
        //rb.AddForce(seekForce);

        // Automatic calculation
        Vector2 seekForce = SeekCurve(rb, mouse, speed);
        rb.AddForce(seekForce);

        // Cheap way to fix this rotation is to orientate in the direction of motion!
        // Homework task 2: add code to prevent the seeker from rotating if its 
        // approximately the same direction as the motion (prevent crazy jitter).
        // Reference Vector3.MoveTowards for the correction threshold.
        Vector3 direction = rb.velocity;
        transform.right = direction;

        //transform.position += transform.right * dt;
        //transform.Translate(Vector3.right * dt);
        //transform.Rotate(0.0f, 0.0f, dt * 100.0f);
        //Debug.Log(Vector3.right);   // Global +x-axis (always [1, 0, 0])
        //Debug.Log(transform.right); // Local +x-axis (changes on-rotation)

        Vector3 xStart, yStart, xEnd, yEnd;
        xStart = yStart = transform.position;

        float length = 10.0f;
        xEnd = xStart + transform.right * length;
        yEnd = yStart + transform.up * length;

        // Ways to rotate objects:
        // 1. transform.Rotate(x, y, z);
        // 2. transform.rotation *= Quaternion.Euler(x, y, z);
        // 3. transform.right = Quaternion.Euler(x, y, z) * Vector3.right;

        //transform.right = Quaternion.Euler(0.0f, 0.0f, 45.0f) * Vector3.right;
        Debug.DrawLine(xStart, xEnd, Color.red);
        Debug.DrawLine(yStart, yEnd, Color.green);
    }
}
