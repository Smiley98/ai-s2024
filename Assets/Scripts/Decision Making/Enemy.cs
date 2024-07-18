using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    public Transform[] waypoints;
    int waypoint = 0;

    const float moveSpeed = 5.0f;
    const float turnSpeed = 1080.0f;
    const float viewDistance = 7.5f;

    [SerializeField]
    GameObject bulletPrefab;
    Timer shootCooldown = new Timer();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootCooldown.total = 0.25f;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float rotation = Steering.RotateTowardsVelocity(rb, turnSpeed, Time.deltaTime);
        rb.MoveRotation(rotation);
        // TODO -- separate logic into states, for now just do stuff unconditionally for ease of testing!

        // Unconditionally seek waypoints & avoid obstacles
        Vector3 steeringForce = Vector2.zero;
        steeringForce += Steering.Seek(rb, waypoints[waypoint].transform.position, moveSpeed);
        // Does more harm than good cause it gets stuck in corners, and detects the player.
        // Could solve with proximity-based weighting, but we're trying to keep this simple!
        //steeringForce += Steering.Avoid(rb, moveSpeed, 2.5f, 15.0f, true);
        rb.AddForce(steeringForce);

        // Unconditionally shoot at the player if visible (LOS)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, viewDistance);
        if (hit && hit.collider.CompareTag("Player"))
        {
            // Shoot at player
            shootCooldown.Tick(dt);
            if (shootCooldown.Expired())
            {
                shootCooldown.Reset();
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = transform.position + transform.right;
                bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 10.0f;
                Destroy(bullet, 1.0f);
            }
        }

        Color color = hit ? Color.red : Color.green;
        Debug.DrawLine(transform.position, transform.position + transform.right * viewDistance, color);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            return;

        waypoint++;
        waypoint %= waypoints.Length;
    }
}
