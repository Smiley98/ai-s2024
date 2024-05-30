using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Steering
{
    SEEK_LINE,
    FLEE_LINE,
    SEEK_CURVE,
    FLEE_CURVE,
    COUNT
}

public class Seek : MonoBehaviour
{
    float speed = 10.0f;
    Rigidbody2D rb;
    Steering behaviour = Steering.SEEK_LINE;

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
        //return -SeekCurve(seeker, target, speed);
        Vector2 cv = seeker.velocity;
        Vector2 dv = (seeker.position - target).normalized * speed;
        return dv - cv;
    }

    void Update()
    {
        // Acquire target
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;

        if (behaviour == Steering.SEEK_LINE)
        {
            transform.position += SeekLine(transform.position, mouse, speed);
        }
        else if (behaviour == Steering.FLEE_LINE)
        {
            transform.position += FleeLine(transform.position, mouse, speed);
        }
        else if(behaviour == Steering.SEEK_CURVE)
        {
            rb.AddForce(SeekCurve(rb, mouse, speed));
        }
        else if(behaviour == Steering.FLEE_CURVE)
        {
            // How to limit a steering behaviour:
            //Vector3 force = FleeCurve(rb, mouse, speed);
            //Vector3.ClampMagnitude(force, speed * 0.1f);
            rb.AddForce(FleeCurve(rb, mouse, speed));
        }

        // Cycle steering behaviours and ensure its within bounds
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int b = (int)behaviour;
            ++b;
            b %= (int)Steering.COUNT;
            behaviour = (Steering)b;
        }
        
        // Behaviour-specific cases
        if (behaviour == Steering.SEEK_LINE || behaviour == Steering.FLEE_LINE)
        {
            rb.velocity = Vector3.zero;
            transform.right = Vector3.right;
        }
        else if (behaviour == Steering.SEEK_CURVE || behaviour == Steering.FLEE_CURVE)
        {
            Vector3 direction = rb.velocity;
            transform.right = direction;
        }
        // Homework task 2: add code to prevent the seeker from rotating if its 
        // approximately the same direction as the motion (prevent crazy jitter).

        // Reference Vector3.MoveTowards for the correction threshold.
        // You only need to handle SEEK_CURVE's orientation for homework.
        // The other 3 behaviours are just examples for how to switch states.
    }
}

// Ways to rotate objects:
// 1. transform.Rotate(x, y, z);
// 2. transform.rotation *= Quaternion.Euler(x, y, z);
// 3. transform.right = Quaternion.Euler(x, y, z) * Vector3.right;