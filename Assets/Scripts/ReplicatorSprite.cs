using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// replicates the sprite renderer, transform, animator, and animation only, igorning everything else.
// TODO: this probably shouldn't be used.
public class ReplicatorSprite : Replicator
{
    // orig
    [Tooltip("The sprite renderer that's being copied. This and related components are the only parts of the original that are kept.")]
    public SpriteRenderer spriteRenderer;

    // grabs the mesh renderer.
    protected override void Awake()
    {
        base.Awake();

        // if the sprite was not set, grab it.
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // replication function.
    public override void Replicate()
    {
        // TODO: this needs to be reworked. It's super inefficient.

        // no sprite renderer to copy
        if (spriteRenderer == null)
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

        // the index of this replicator in the component list of replicators.
        int repIndex = -1;

        // disables the replicators that have already been gone through.
        {
            // gets all replicators
            Replicator[] reps = gameObject.GetComponents<Replicator>();

            // disables all replicators that have already won.
            for (int i = 0; i < reps.Length; i++)
            {
                repIndex = i;

                // finds the index  of the current replicator.
                if (reps[i] == this)
                    break;
            }
        }

        // OBJECTS //
        // makes copy of game object.
        GameObject spriteObject = Instantiate(spriteRenderer.gameObject);

        // gets all the components from the object copy.
        {
            List<Component> comps = new List<Component>();
            spriteObject.GetComponents(comps); // gets all the components.

            // this doesn't actually destroy the component. It just shuts it off.
            // deletes all the components except the ones that should be kept.
            for (int i = 0; i < comps.Count; i++)
            {
                // TODO: check if there are other components to keep.
                // if it's the sprite renderer, keep it.
                // it also ignores animations.
                if (!(comps[i].GetType() == typeof(Transform) ||
                    comps[i].GetType() == typeof(SpriteRenderer) || comps[i].GetType() == typeof(Sprite) ||
                    comps[i].GetType() == typeof(Animator) || comps[i].GetType() == typeof(Animation)))
                {
                    Destroy(comps[i]); // destroy the component. This actually just turns it off.
                }
            }
        }

        // make iterations
        for (uint i = 1; i <= totalIterations; i++)
        {
            // copy sprite object.
            GameObject copy = Instantiate(spriteObject);

            if (i == 1) // use base sprite.
                copy = spriteObject;

            // disables used replicator components by setting 'allowReplications' on them to 'false'.
            {
                // grabs the replications from the copy.
                Replicator[] copyReps = copy.gameObject.GetComponents<Replicator>();

                // stops used replications form being used.
                for (int j = 0; j < repIndex; j++)
                    copyReps[j].allowReplications = false;
            }

            // the direction
            Vector3 direc = GetDirection();

            // spaces out the copy.
            if (applyScaleForSpacing) // account for object's scale
            {
                // calculates hte direction by the object's scale.
                Vector3 direcScaled = direc.normalized;
                direcScaled.Scale(transform.localScale);

                copy.transform.position += direcScaled * spacing * i;
            }
            else // ignore object's scale
            {
                copy.transform.position += direc.normalized * spacing * i;
            }

            // translates the copy by the offset.
            copy.transform.Translate(offset * i);

            // if the parent is the parent.
            if (originalAsParent)
                copy.transform.parent = spriteRenderer.gameObject.transform;
        }

        // GenerateIterations(this, meshRenderer.gameObject.transform);

        // destroys the temporary mesh object.
        Destroy(spriteObject);
    }
}
