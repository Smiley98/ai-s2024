using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSeek : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;
        transform.position = mouse;
    }
}
