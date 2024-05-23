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

    // How Vector3.MoveTowards works internally
    Vector3 SeekLine(Vector3 A, Vector3 B, float speed)
    {
        // AB = B - A
        Vector3 direction = (B - A).normalized;
        float distance = speed * Time.deltaTime;
        return direction * distance;
        // TODO -- clamp distance
    }

    // Homework: turn this into a static method that can be applied to any
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
    }
}
