using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the 3D world.
public class World3D : MonoBehaviour
{
    // the bounds of the world.
    public Vector3 boundsMin = new Vector3(-50.0F, -50.0F, -50.0F);
    public Vector3 boundsMax = new Vector3(50.0F, 50.0F, 50.0F);

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
