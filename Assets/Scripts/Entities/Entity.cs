using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// entity for the game.
public class Entity : MonoBehaviour
{
    // the name and description of the entities.
    public string entityName = "";
    public string description = "";

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Checks which object is closest to the origin.
    public GameObject ClosestObject(GameObject origin, GameObject object1, GameObject object2)
    {
        // The closest object.
        GameObject closest;

        // Gets the two distances.
        float dist1 = (object1.transform.position - origin.transform.position).magnitude;
        float dist2 = (object2.transform.position - origin.transform.position).magnitude;

        // Checks which one is closest.
        if(dist1 >= dist2)
            closest = object1;
        else
            closest = object2;

        // Returns the closest object.
        return closest;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
