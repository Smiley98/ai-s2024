using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
    // Probes are 30 degrees away from direction (transform.right, local x-axis)
    float probeAngle = 30.0f;
    float speed = 5.0f;
    Rigidbody2D rb;

    Vector3 SeekCurve(Rigidbody2D seeker, Vector2 target, float speed)
    {
        Vector2 cv = seeker.velocity;
        Vector2 dv = (target - seeker.position).normalized * speed;
        return dv - cv;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;
        //rb.AddForce(SeekCurve(rb, mouse, speed));

        float dt = Time.deltaTime;
        float distance = 5.0f;

        // Move +-30 degrees of our **current** (transform.right) direction.
        Quaternion leftRotation = Quaternion.Euler( 0.0f, 0.0f,  probeAngle);
        Quaternion rightRotation = Quaternion.Euler(0.0f, 0.0f, -probeAngle);
        Vector3 leftDirection = leftRotation   * transform.right;
        Vector3 rightDirection = rightRotation * transform.right;

        //Debug.DrawLine(transform.position, transform.position + direction * distance, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + leftDirection  * distance, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + rightDirection * distance, Color.magenta);

        // Avoid abstacle if hit!
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        //if (hit.collider != null)
        //{
        //    Vector2 avoidPosition = transform.position + transform.up * 5.0f;
        //    rb.AddForce(SeekCurve(rb, avoidPosition, speed));
        //
        //    Quaternion.RotateTowards(transform.rotation, leftRotation, 1.0f * dt);
        //    transform.right = rb.velocity;
        //}

        // Same values, just one uses local axes and the other global
        //Vector3 from = transform.rotation * Vector3.right;
        //Vector3 to = transform.rotation * leftRotation * Vector3.right;
        //Vector3 from = transform.right;
        //Vector3 to = leftRotation * transform.right;
        //Debug.Log(from);
        //Debug.Log(to);

        // Rotates towards a direction 30 degrees to the left of our player
        // at rate of no more than 10 degrees per second
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * leftRotation, 10.0f * dt);
    }
}
