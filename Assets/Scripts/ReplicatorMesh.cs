using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// replicates the mesh filter, mesh renderer, transform, animator, and animation only, igorning everything else.
public class ReplicatorMesh : Replicator
{
    // orig
    [Tooltip("The mesh renderer that's being copied. This and related components are the only parts of the original that are kept.")]
    public MeshRenderer meshRenderer;

    // grabs the mesh renderer.
    protected override void Awake()
    {
        base.Awake();

        // if the mesh was not set, grab it.
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    // replication function.
    public override void Replicate()
    {
        // no mesh to copy
        if (meshRenderer == null)
        {
            Debug.LogAssertion("No MeshRenderer component provided.");
            return;
        }

        // not the original
        if (iteration != 0)
        {
            Debug.LogAssertion("Can only copy from the original.");
            return;
        }

        // OBJECTS //
        // makes copy of game object.
        GameObject meshObject = Instantiate(meshRenderer.gameObject);

        // gets all the components from the object copy.
        {
            List<Component> comps = new List<Component>();
            meshObject.GetComponents(comps); // gets all the components.

            // this doesn't actually destroy the component. It just shuts it off.
            // deletes all the components except the ones that should be kept.
            for (int i = 0; i < comps.Count; i++)
            {
                // if it's the mesh renderer or the mesh filter, keep it.
                // it also ignores animations.
                if(!(comps[i].GetType() == typeof(Transform) ||
                    comps[i].GetType() == typeof(MeshRenderer) || comps[i].GetType() == typeof(MeshFilter) || 
                    comps[i].GetType() == typeof(Animator) || comps[i].GetType() == typeof(Animation)))
                {
                    Destroy(comps[i]); // destroy the component. This actually just turns it off.
                }
            }
        }

        // make iterations
        for (uint i = 1; i <= totalIterations; i++)
        {
            // copy mesh object.
            GameObject copy = Instantiate(meshObject);
        
            if (i == 1) // use base mesh.
                copy = meshObject;
        
            // the direction
            Vector3 direc = GetDirection();
        
            // spaces out the copy.
            copy.transform.position += direc.normalized * spacing * i;
        
            // translates the copy by the offset.
            copy.transform.Translate(offset * i);
        
            // if the parent is the parent.
            if (originalAsParent)
                copy.transform.parent = meshRenderer.gameObject.transform;
        }

        // GenerateIterations(this, meshRenderer.gameObject.transform);

        // destroys the temporary mesh object.
        Destroy(meshObject);
    }
}
