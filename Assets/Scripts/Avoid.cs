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
        // Lab 3 (4% total) -- Add 2 probes (2% per probe)
        // Ensure each probe detects the obstacle, and avoids it accordingly.
        // (Handling rotation is not required. As long as the position changes, you're good)!
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;
        rb.AddForce(SeekCurve(rb, mouse, speed));

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

        RaycastHit2D hitLeft  = Physics2D.Raycast(transform.position, leftDirection,  distance);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightDirection, distance);

        // Seek right if obstacle detected to the left
        if (hitLeft)
        {
            Vector2 avoidPosition = transform.position - transform.up * 5.0f;
            rb.AddForce(SeekCurve(rb, avoidPosition, speed));
        }
        // Seek left if obstacle detected to the right
        else if (hitRight)
        {
            Vector2 avoidPosition = transform.position + transform.up * 5.0f;
            rb.AddForce(SeekCurve(rb, avoidPosition, speed));
        }

        // Bonus TODO:
        // Play with this -- seems avoidance rotates accordingly, but cursor seek isn't rotating correctly
        //float angle = Vector2.SignedAngle(Vector3.right, rb.velocity.normalized);
        //Quaternion from = transform.rotation;
        //Quaternion to = from * Quaternion.Euler(0.0f, 0.0f, angle);
        //float maxAngle = 100.0f * dt;
        //transform.rotation = Quaternion.RotateTowards(from, to, maxAngle);

        // This also works reasonable well!
        //transform.right = rb.velocity;
    }
}
