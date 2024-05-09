using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSeek : MonoBehaviour
{
    // Move at a speed of 5 units per second
    float speed = 5.0f;

    void Start()
    {
        
    }

    // Move the object towards the mouse cursor
    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;
        transform.position = Vector3.MoveTowards(transform.position, mouse, speed * Time.deltaTime);
    }
}
