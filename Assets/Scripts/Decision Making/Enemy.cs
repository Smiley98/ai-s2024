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

    const float moveSpeed = 7.5f;
    const float turnSpeed = 1080.0f;
    const float viewDistance = 5.0f;

    [SerializeField]
    GameObject bulletPrefab;
    Timer shootCooldown = new Timer();

    enum State
    {
        NEUTRAL,
        OFFENSIVE,
        DEFENSIVE
    };

    // TODO: Add health to player & enemy.
    // If enemy drops below 25% health, flee and shoot!
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

        float playerDistance = Vector2.Distance(transform.position, player.position);
        state = playerDistance <= viewDistance ? State.OFFENSIVE : State.NEUTRAL;
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

        Color color = state == State.NEUTRAL ? Color.green : Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.right * viewDistance, color);
    }

    void Attack()
    {
        // Seek player
        Vector3 steeringForce = Vector2.zero;
        steeringForce += Steering.Seek(rb, player.position, moveSpeed);
        rb.AddForce(steeringForce);

        // LOS to player
        Vector3 playerDirection = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, viewDistance);
        bool playerHit = hit && hit.collider.CompareTag("Player");

        // Shoot player if in LOS
        shootCooldown.Tick(Time.deltaTime);
        if (playerHit && shootCooldown.Expired())
        {
            shootCooldown.Reset();
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + playerDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = playerDirection * 10.0f;
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
