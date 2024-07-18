using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Rigidbody2D rb;

    public Transform[] waypoints;
    int waypoint = 0;

    const float moveSpeed = 5.0f;
    const float turnSpeed = 1080.0f;
    const float viewDistance = 7.5f;

    [SerializeField]
    GameObject bulletPrefab;
    Timer shootCooldown = new Timer();

    enum State
    {
        NEUTRAL,
        OFFENSIVE,
        DEFENSIVE
    };

    State state = State.NEUTRAL;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootCooldown.total = 0.25f;
    }

    void Update()
    {
        float rotation = Steering.RotateTowardsVelocity(rb, turnSpeed, Time.deltaTime);
        rb.MoveRotation(rotation);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, viewDistance);
        bool playerHit = hit && hit.collider.CompareTag("Player");
        state = playerHit ? State.OFFENSIVE : State.NEUTRAL;
        // TODO: Add transition state-based actions (ie acquire nearest waypoint).

        // Repeating state-based actions:
        switch (state)
        {
            case State.NEUTRAL:
                Patrol();
                break;

            case State.OFFENSIVE:
                Attack();
                break;
        }

        Color color = playerHit ? Color.red : Color.green;
        Debug.DrawLine(transform.position, transform.position + transform.right * viewDistance, color);
    }

    void Attack()
    {
        // Seek player
        Vector3 steeringForce = Vector2.zero;
        steeringForce += Steering.Seek(rb, player.position, moveSpeed);
        rb.AddForce(steeringForce);

        // Shoot player
        shootCooldown.Tick(Time.deltaTime);
        if (shootCooldown.Expired())
        {
            shootCooldown.Reset();
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + transform.right;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 10.0f;
            Destroy(bullet, 1.0f);
        }
    }

    void Patrol()
    {
        // Seek waypoint
        Vector3 steeringForce = Vector2.zero;
        steeringForce += Steering.Seek(rb, waypoints[waypoint].transform.position, moveSpeed);
        rb.AddForce(steeringForce);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            return;

        // Extra practice: ensure the player will always patrol the nearest waypoint.
        // Keep track of this when switching into the NEUTRAL state (instead of whenever the player collides)!
        waypoint++;
        waypoint %= waypoints.Length;
    }
}
