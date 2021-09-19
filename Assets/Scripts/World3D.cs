using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the 3D world.
public class World3D : MonoBehaviour
{
    // the grid for the 3D world. The grid is attached to the terrain.
    // by default, all cells are 10 units apart. By extension, cubes are made 10 units apart as well.
    // also by default, 1 Unity unit is equal to 1 metre (i.e. 100 centimetres)
    public Grid grid;

    // world dimensions (cannot be changed after the game starts)
    // make sure the size stays even.
    public Vector3Int worldSize = new Vector3Int(10, 10, 10);

    // grid for grass locations.
    // this is 2D since there's only one grass block per z-cell position.
    // true = grass placed, false = no grass.
    private bool[,] grassGrid;

    // Start is called before the first frame update
    void Start()
    {
        // the default world size.
        int defWorldSize = 10;

        // the world has no x-dimension
        if (worldSize.x == 0)
        {
            Debug.LogWarning("No world X set. Giving default value");
            worldSize.x = defWorldSize;
        }
            
        // the world has no y-dimension
        if (worldSize.y == 0)
        {
            Debug.LogWarning("No world Y set. Giving default value");
            worldSize.y = defWorldSize;
        }
            
        
        // makes grass grid.
        grassGrid = new bool[worldSize.x, worldSize.y];

        // gridSize.x = Mathf.Abs(gridSize.x);
        // gridSize.y = Mathf.Abs(gridSize.y);
        // grid = new bool[gridSize.x, gridSize.y];
    }

    // gets the minimum based on the world's location.
    public Vector3 GetWorldMinimum()
    {
        // grid exists.
        if (grid != null)
        {
            return grid.CellToWorld(-worldSize / 2);
        }
        else // grid does not exist.
        {
            Debug.LogError("No grid set. Returning zero vector.");
            return Vector3.zero;
        }
    }

    // gets the maximum based on the world's location.
    public Vector3 GetWorldMaximum()
    {
        // grid exists.
        if (grid != null)
        {
            return grid.CellToWorld(worldSize / 2);
        }
        else // grid does not exist.
        {
            Debug.LogError("No grid set. Returning zero vector.");
            return Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // if(grid != null)
        // {
        //     Vector3 zero = new Vector3(100, 0, 0);
        //     Vector3 cell = grid.WorldToCell(zero);
        //     Debug.Log("(100, 0, 0) = " + cell.ToString());
        // }
        
    }
}
