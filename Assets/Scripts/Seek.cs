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

    Vector3 SeekLine(Vector3 A, Vector3 B, float speed)
    {
        Vector3 direction = (B - A).normalized;
        float distance = speed * Time.deltaTime;
        return direction * distance;
    }

    Vector3 FleeLine(Vector3 A, Vector3 B, float speed)
    {
        return -SeekLine(A, B, speed);
    }

    // Homework task 1: turn this into a static method that can be applied to any
    // GameObject which has all the data needed to seek (rb & speed)
    Vector3 SeekCurve(Rigidbody2D seeker, Vector2 target, float speed)
    {
        Vector2 cv = seeker.velocity;
        Vector2 dv = (target - seeker.position).normalized * speed;
        return dv - cv;
    }

    Vector3 FleeCurve(Rigidbody2D seeker, Vector2 target, float speed)
    {
        return -SeekCurve(seeker, target, speed);
    }

    void Update()
    {
        // Acquire target
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;

        transform.position += FleeLine(transform.position, mouse, speed);

        // Seek target (position only, no rotation)
        //Vector2 seekForce = SeekCurve(rb, mouse, speed);
        //rb.AddForce(seekForce);

        // Point at target
        Vector3 direction = rb.velocity;
        transform.right = direction;
        // Homework task 2: add code to prevent the seeker from rotating if its 
        // approximately the same direction as the motion (prevent crazy jitter).
        // Reference Vector3.MoveTowards for the correction threshold.

        // Render local axes
        float length = 10.0f;
        Vector3 xStart, yStart, xEnd, yEnd;
        xStart = yStart = transform.position;
        xEnd = xStart + transform.right * length;
        yEnd = yStart + transform.up * length;
        Debug.DrawLine(xStart, xEnd, Color.red);
        Debug.DrawLine(yStart, yEnd, Color.green);
    }
}

// Ways to rotate objects:
// 1. transform.Rotate(x, y, z);
// 2. transform.rotation *= Quaternion.Euler(x, y, z);
// 3. transform.right = Quaternion.Euler(x, y, z) * Vector3.right;