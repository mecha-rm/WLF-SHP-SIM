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
        // finds the world.
        if (world == null)
            world = FindObjectOfType<World3D>();

        // no world exists.
        if(world == null)
        {
            Debug.LogAssertion("Grass cannont grow without an established world.");
            return;
        }

        // array that represents all 8 possible locations for new grass.
        bool[,] spreadArr = new bool[3, 3];

        // checks each section.
        // row
        // for(int i = 0; i < spreadArr.Length; i++)
        // {
        //     // col
        //     for(int j = 0; j < spreadArr.GetLength(0); j++)
        //     {
        // 
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
