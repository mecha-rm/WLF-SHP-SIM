using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Living
{
    // Grass variables
    [Header("Grass")]

    // sets the life span of the grass.
    // static so that it applies for all grass.
    private static float LIFE_SPAN = 9999;

    // world for the grass to grow on
    public World3D world;

    [Header("Grass/Bounds")]
    // if 'true', the bounds of the mesh are applied.
    public bool applyMeshBounds = true;

    // the bounds of the world.
    public Vector3 boundsMin = new Vector3(-1.0F, -1.0F, -1.0F);
    public Vector3 boundsMax = new Vector3(1.0F, 1.0F, 1.0F);
    public Vector3 boundsCenter = new Vector3(0.0F, 0.0F, 0.0F);

    [Header("Grass/Spread")]
    // grass can spread
    public bool canSpread = true;

    // time for grass to spread.
    public float spreadWaitTime = 50.0F;

    // spread timer
    public float spreadTimer = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        species = "grass";
        lifeSpan = LIFE_SPAN;
        lifeExpect = LIFE_SPAN;

        // gets the mesh renderer
        MeshRenderer mr = GetComponent<MeshRenderer>();

        // mesh renderer found.
        if (mr != null)
        {
            // gets the bounds of the model.
            boundsMin = mr.bounds.min; // get max
            boundsMax = mr.bounds.max; // get min
            boundsCenter = mr.bounds.center; // get center
        }
    }

    // spreads the grass.
    private void Spread()
    {
        // TODO: rework to align with World3D Grid.
        // match with grid location centers intead.

        // finds the world.
        // if (world == null)
        //     world = FindObjectOfType<World3D>();
        // 
        // // no world exists.
        // if(world == null)
        // {
        //     Debug.LogAssertion("Grass cannont grow without an established world.");
        //     return;
        // }
        // 
        // // array that represents all 8 possible locations for new grass.
        // bool[,] spreadArr = new bool[3, 3];
        // 
        // 
        // // checks each section.
        // // row
        // // for(int i = 0; i < spreadArr.Length; i++)
        // // {
        // //     // col
        // //     for(int j = 0; j < spreadArr.GetLength(0); j++)
        // //     {
        // //        
        // //     }
        // // }
        // 
        // // offset for grass
        // Vector3 offset = boundsMax - boundsCenter;
        // 
        // // x (horizontal)
        // for (int x = -1; x < 1; x++)
        // {
        //     // z (vertical)
        //     for (int z = -1; z < 1; z++)
        //     {
        //         // current location.
        //         if (x == 0 && z == 0)
        //             continue;
        // 
        //         // calculates next position and farthest point.
        //         Vector3 nextPos = transform.position + new Vector3(offset.x * x, 0.0F, offset.z * z);
        //         Vector3 FarthestPoint = nextPos + new Vector3(offset.x * x, 0.0F, offset.z * z);
        // 
        //         // checks farthest point to see if it's in the world bounds.
        //         if(world.InWorldBounds(FarthestPoint))
        //         {
        //             // TODO: check for collision with other grass.
        //             Grass grass = EntityManager.GetInstance().CreateGrass();
        // 
        //             // puts grass in next position.
        //             if (grass != null)
        //                 grass.transform.position = nextPos;
        //         }
        //     }
        // }

    }

    // grass has killed something (this should never be called).
    public override void Kills(GameObject victim)
    {
        // throw new System.NotImplementedException();
    }

    // grass has been killed (either eaten or aged to death).
    public override void OnKilled(GameObject killer)
    {
        // throw new System.NotImplementedException();
    }


    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
