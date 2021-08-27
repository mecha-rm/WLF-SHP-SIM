using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the 3D world.
public class World3D : MonoBehaviour
{
    // the bounds of the world.
    public Vector3 boundsMin = new Vector3(-50.0F, -50.0F, -50.0F);
    public Vector3 boundsMax = new Vector3(50.0F, 50.0F, 50.0F);

    // public Vector2Int gridSize;
    // private bool[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        // gridSize.x = Mathf.Abs(gridSize.x);
        // gridSize.y = Mathf.Abs(gridSize.y);
        // grid = new bool[gridSize.x, gridSize.y];
    }

    // gets the center of the world.
    public Vector3 GetLocalBoundsCenter()
    {
        return (boundsMin + boundsMax) / 2.0F;
    }

    // gets the minimum based on the world's location.
    public Vector3 GetWorldBoundsMinimum()
    {
        return transform.position + boundsMin;
    }

    // gets the maximum based on the world's location.
    public Vector3 GetWorldBoundsMaximum()
    {
        return transform.position + boundsMax;
    }

    // gets the center based on the world's location.
    public Vector3 GetWorldBoundsCenter()
    {
        return (2 * transform.position + boundsMin + boundsMax) / 2.0F;
    }

    // check to see if the position is within the world bounds.
    public bool InWorldBounds(Vector3 pos)
    {
        return (
            pos.x >= boundsMin.x && pos.x <= boundsMax.x &&
            pos.y >= boundsMin.y && pos.y <= boundsMax.y &&
            pos.z >= boundsMin.z && pos.z <= boundsMax.z
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
