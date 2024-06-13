using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    int rows = 10;
    int cols = 20;
    List<List<GameObject>> tileObjects = new List<List<GameObject>>();
    int[,] tiles =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };

    void Start()
    {
        float xStart = 0.5f;
        float yStart = rows - 0.5f;
        float x = xStart;
        float y = yStart;

        for (int row = 0; row < rows; row++)
        {
            // Create a new list of tiles for each row
            tileObjects.Add(new List<GameObject>());

            // Add a tile to the list for each column of the row
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, y);
                tileObjects[row].Add(tile);

                x += 1.0f;
            }

            // Move to start of next row
            x = xStart;
            y -= 1.0f;
        }
    }

    void Update()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                //ColorTile(tileObjects[row][col], Color.white);
                ColorTile(new Cell { col = col, row = row });
            }
        }

        ColorTile(tileObjects[0][0], Color.red);                  // top-left
        ColorTile(tileObjects[0][cols - 1], Color.green);         // top-right
        ColorTile(tileObjects[rows - 1][cols - 1], Color.blue);   // bot-right
        ColorTile(tileObjects[rows - 1][0], Color.magenta);       // bot-left

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;

        Cell mouseCell = WorldToGrid(mouse);
        ColorTile(tileObjects[mouseCell.row][mouseCell.col], Color.cyan);

        // If you implement adjacents correctly, you'll see a "plus" around your cursor!
        foreach (Cell adj in Pathing.Adjacents(mouseCell, rows, cols))
        {
            ColorTile(tileObjects[adj.row][adj.col], Color.magenta);
        }
    }
    
    void ColorTile(GameObject tile, Color color)
    {
        tile.GetComponent<SpriteRenderer>().color = color;
    }

    void ColorTile(Cell cell)
    {
        int value = tiles[cell.row, cell.col];
        Color[] colors =
        {
            Color.gray,     // 0 = Air
            Color.black,    // 1 = Wall
            Color.blue,     // 2 = Water
            Color.green     // 3 = Grass
        };
        ColorTile(tileObjects[cell.row][cell.col], colors[value]);
    }

    // Hint: Clamp will be helpful for Task 1!
    Cell WorldToGrid(Vector3 world)
    {
        int col = (int)world.x;
        int row = (rows - 1) - (int)world.y;
        col = Mathf.Clamp(col, 0, cols - 1);
        row = Mathf.Clamp(row, 0, rows - 1);
        return new Cell { col = col, row = row };
    }
}
