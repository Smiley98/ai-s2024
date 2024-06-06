using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    int rows = 10;
    int cols = 20;
    List<List<GameObject>> tiles = new List<List<GameObject>>();

    void Start()
    {
        float xStart = 0.5f;
        float yStart = rows - 0.5f;
        float x = xStart;
        float y = yStart;

        for (int row = 0; row < rows; row++)
        {
            // Create a new list of tiles for each row
            tiles.Add(new List<GameObject>());

            // Add a tile to the list for each column of the row
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, y);
                tiles[row].Add(tile);

                x += 1.0f;
            }
            x = xStart;
            y -= 1.0f;
        }

        
    }

    void Update()
    {
        ColorTile(tiles[0][0], Color.red);                  // top-left
        ColorTile(tiles[0][cols - 1], Color.green);         // top-right
        ColorTile(tiles[rows - 1][cols - 1], Color.blue);   // bot-right
        ColorTile(tiles[rows - 1][0], Color.magenta);       // bot-left     
    }
    
    void ColorTile(GameObject tile, Color color)
    {
        tile.GetComponent<SpriteRenderer>().color = color;
    }
}
