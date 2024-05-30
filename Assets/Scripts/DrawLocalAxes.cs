using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLocalAxes : MonoBehaviour
{
    void Update()
    {
        float length = 10.0f;
        Vector3 xStart, yStart, xEnd, yEnd;
        xStart = yStart = transform.position;
        xEnd = xStart + transform.right * length;
        yEnd = yStart + transform.up * length;
        Debug.DrawLine(xStart, xEnd, Color.red);
        Debug.DrawLine(yStart, yEnd, Color.green);
    }
}
