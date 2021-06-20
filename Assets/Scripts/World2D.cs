using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World2D : MonoBehaviour
{
    // the grid size (x = cols, y = rows)
    public Vector2Int gridSize = new Vector2Int(32, 32);

    Vector2[,] arr;

    // Start is called before the first frame update
    void Start()
    {
        // x = column count
        // y = row count
        arr = new Vector2[gridSize.y, gridSize.x];

    }

    // resets the world and keeps the existing gridSize
    public void ResetWorld(int rowCount, int colCount)
    {
        // the row and column count cannot be less than 0.
        if(rowCount <= 0 || colCount <= 0)
        {
            Debug.LogWarning("The row count nor column count can be set to zero. Keeping original values.");
        }
        else
        {
            gridSize.x = colCount;
            gridSize.y = rowCount;
        }

        // changing size of world
        arr = new Vector2[gridSize.y, gridSize.x];
    }

    // resets the world.
    public void ResetWorld()
    {
        ResetWorld(gridSize.y, gridSize.x);
    }



    // resizes the world
    // public void ResizeWorld(int newRows, int newCols)
    // {
    //     if(newRows <= 0 || newCols <= 0)
    //     {
    //         Debug.LogError("The row and column count cannot be zero or less.");
    //         return;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
