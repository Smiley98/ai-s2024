using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Rigidbody2D rb;

    [SerializeField]
    Transform[] waypoints;
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
        Color color = Color.white;
        switch (state)
        {
            case State.NEUTRAL:
                Patrol();
                color = Color.magenta;
                break;

            case State.OFFENSIVE:
                Attack();
                color = Color.red;
                break;

            case State.DEFENSIVE:
                Defend();
                color = Color.blue;
                break;
        }

        GetComponent<SpriteRenderer>().color = color;
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

    void Defend()
    {
        // TODO -- flee
        // TODO -- supressing fire
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
        // You might want to add an EnemyBullet vs PlayerBullet tag, maybe even remove the Bullet tag.
        if (collision.CompareTag("Bullet"))
        {
            // TODO -- damage enemy if it gets hit with a *Player* bullet
            // (be careful not to damage the enemy if it collides with its own bullets)
        }

        if (collision.CompareTag("Waypoint"))
        {
            waypoint++;
            waypoint %= waypoints.Length;
        }
    }
}
