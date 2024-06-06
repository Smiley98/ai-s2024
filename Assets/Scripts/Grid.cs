using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    List<GameObject> tiles = new List<GameObject>();

    void Start()
    {
        int cols = 20;
        float xStart = 0.5f;
        float yStart = 0.5f;
        float x = xStart;
        float y = yStart;
        for (int col = 0; col < cols; col++)
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.transform.position = new Vector3(x, y);
            tiles.Add(tile);

            x += 1.0f;
        }
    }

    void Update()
    {
        
    }
}
