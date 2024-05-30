using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = 5.0f;
        Vector3 direction = transform.right;
        Debug.DrawLine(transform.position, transform.position + direction * distance, Color.magenta);

        // Avoid abstacle if hit!
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            transform.Rotate(0.0f, 0.0f, 10.0f * Time.deltaTime);
        }
    }
}
