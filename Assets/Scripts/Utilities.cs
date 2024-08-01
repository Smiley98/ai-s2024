using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    // Finds the index of the transform nearest to position
    public static int NearestPosition(Vector3 position, Transform[] transforms)
    {
        int nearest = 0;
        float minDistance = float.MaxValue;
        for (int i = 0; i < transforms.Length; i++)
        {
            float distance = Vector2.Distance(position, transforms[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = i;
            }
        }
        return nearest;
    }
}
