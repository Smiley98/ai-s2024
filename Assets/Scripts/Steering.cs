using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SteeringBehaviour
{
    SEEK,
    FLEE,
    COUNT
}

public class Steering : MonoBehaviour
{
    public static Vector3 Seek(Rigidbody2D seeker, Vector2 target, float speed)
    {
        Vector2 current = seeker.velocity;
        Vector2 desired = (target - seeker.position).normalized * speed;
        return desired - current;
    }

    public static Vector3 Flee(Rigidbody2D seeker, Vector2 target, float speed)
    {
        Vector2 current = seeker.velocity;
        Vector2 desired = (seeker.position - target).normalized * speed;
        return desired - current;
    }

    SteeringBehaviour behaviour = SteeringBehaviour.SEEK;
    Rigidbody2D rb;
    float moveSpeed = 10.0f;
    float turnSpeed = 1080.0f;
    float rayLength = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Acquire target
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;

        // 2. Linear seek
        Vector3 steeringForce = Vector3.zero;
        switch (behaviour)
        {
            case SteeringBehaviour.SEEK:
                steeringForce = Seek(rb, mouse, moveSpeed);
                break;

            case SteeringBehaviour.FLEE:
                steeringForce = Flee(rb, mouse, moveSpeed);
                break;
        }
        rb.AddForce(steeringForce);

        // 3. Angular seek
        float currentRotation = rb.rotation;
        float desiredRotation = Vector2.SignedAngle(Vector3.right, rb.velocity.normalized);
        float deltaRotation = turnSpeed * Time.deltaTime;
        float rotation = Mathf.MoveTowardsAngle(currentRotation, desiredRotation, deltaRotation);
        rb.MoveRotation(rotation);

        // Draw local axes
        Debug.DrawLine(transform.position, transform.position + transform.right * 10.0f, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.up * 10.0f, Color.green);
    }
}
