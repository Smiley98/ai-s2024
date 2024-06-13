using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cell
{
    public int col;
    public int row;
}

public static class Pathing
{
    public static List<Cell> FloodFill(Cell start, Cell end, int[,] tiles, int count, Grid grid = null)
    {
        int rows = tiles.GetLength(0);
        int cols = tiles.GetLength(1);

        Queue<Cell> frontier = new Queue<Cell>();
        frontier.Enqueue(start);
        for (int i = 0; i < count; i++)
        {
            Cell cell = frontier.Dequeue();

            if (grid != null)
                grid.ColorTile(cell, Color.magenta);

            foreach (Cell adj in Adjacents(cell, rows, cols))
            {
                frontier.Enqueue(adj);
            }
        }

        // Using grid as a debug renderer via ColorTile
        if (grid != null)
        {
            // Remember to make start & end the correct colours (1%) ;)
            grid.ColorTile(start, Color.green);
            grid.ColorTile(end, Color.red);
        }

        List<Cell> path = new List<Cell>();
        return path;
    }

    // Task 1: Follow the pseudocode to create an algorithm that makes a list of cells
    // which are adjacent (left, right, up, down) of the passed in cell.
    // *Ensure cells do not cause out-of-bounds errors (> 0, < rows & cols)*
    public static List<Cell> Adjacents(Cell cell, int rows, int cols)
    {
        List<Cell> cells = new List<Cell>();

        // left-case
        if (cell.col - 1 >= 0)
            cells.Add(new Cell { col = cell.col - 1, row = cell.row });

        // down-case
        if (cell.row + 1 < rows)
            cells.Add(new Cell { col = cell.col, row = cell.row + 1 });

        // if the cell above the current cell is within bounds, add it to the list
        // if the cell below the current cell is within bounds, add it to the list
        // if the cell left of the current cell is within bounds, add it to the list
        // if the cell right of the current cell is within bounds, add it to the list
        return cells;
    }
}