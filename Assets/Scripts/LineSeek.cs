using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSeek : MonoBehaviour
{
    void Start()
    {
        
    }

    // Make object position the position of the mouse cursor
    void Update()
    {
        // 1. Mouse position in screen space
        Vector3 mouse = Input.mousePosition;

        // 2. Convert mouse position to screen space
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse = new Vector3(mouse.x, mouse.y, 0.0f);

        // 3. Use world-space mouse to move our object!
        transform.position = mouse;
        Debug.Log(mouse);
    }
}
