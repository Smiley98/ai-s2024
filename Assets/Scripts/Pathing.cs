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
