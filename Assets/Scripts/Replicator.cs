using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes a line of copies of a provided object. This is based on the 'Array' modifier in Blender.
// TODO: maybe make it so that you can make a copy of a copy?
public class Replicator : MonoBehaviour
{
    // the direction of the replicator.
    public enum repDirec { PositiveX, NegativeX, PositiveY, NegativeY, PositiveZ, NegativeZ};

    // NOTE: to avoid an infinite loop, this script must be attached to the object being replicated.
    // the original that is being replicated.
    [Tooltip("The original object being replicated. If left as null, it is set to the object it's attached to.")]
    private Replicator original = null;

    // if 'true', the original is used as the object's parent.
    [Tooltip("If true, the copies have the original as their parent transform.")]
    public bool originalAsParent = false;

    [Tooltip("The number of the generated copy. The original has an 'iteration' of 0.")]
    public uint iteration = 0;

    // the total amount of iterations that will occur. This is irrelevant for anyone but iteration 0.
    [Tooltip("The total amount of iterations to do. This is only relevant to iteration 0.")]
    public uint totalIterations = 0;

    [Header("Spacing")]

    // the replicator direction.
    [Tooltip("The direction that the replications are translated in.")]
    public repDirec direction;

    // if 'true', the direction is relative to the original object.
    // if 'false', the direction is relative to the standard Vector.
    [Tooltip("If true, shift objects relative to the original's orientation.")]
    public bool relativeToOriginal = true;

    // spacing of the object.
    [Tooltip("The spacing between the copies along the set direction.")]
    public float spacing = 1.0F;

    // replication offsets
    [Tooltip("Offsets the position of the copy after 'spacing' is applied.")]
    public Vector3 offset = new Vector3(0.0F, 0.0F, 0.0F);

    // if 'true', the repliction is triggered at the start.
    public bool triggerOnStart = true;

    // Awake is called when the script instance is being loaded.
    protected virtual void Awake()
    {
        // original not set.
        if (original == null)
            original = this;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // calls for replication.
        if(triggerOnStart && iteration == 0 && totalIterations != 0)
            Replicate();
    }

    // returns original object.
    public Replicator Original
    {
        get
        {
            return original;
        }
    }

    // gets the direction the replicas will go in.
    public Vector3 GetDirection()
    {
        // the direction
        Vector3 direc;

        // chooses the direction.
        switch (direction)
        {
            default:
            case repDirec.PositiveX: // +x
                direc = (relativeToOriginal) ? original.transform.right : Vector3.right;
                break;

            case repDirec.NegativeX: // -x
                direc = (relativeToOriginal) ? -original.transform.right : Vector3.left;
                break;

            case repDirec.PositiveY: // +y
                direc = (relativeToOriginal) ? original.transform.up : Vector3.up;
                break;

            case repDirec.NegativeY: // -y
                direc = (relativeToOriginal) ? -original.transform.up : Vector3.down;
                break;

            case repDirec.PositiveZ: // +z
                direc = (relativeToOriginal) ? original.transform.forward : Vector3.forward;
                break;

            case repDirec.NegativeZ: // -z
                direc = (relativeToOriginal) ? -original.transform.forward : Vector3.back;
                break;
        }

        return direc;
    }

    // replicates the object.
    public virtual void Replicate()
    {
        // no object to copy
        if(original == null)
        {
            Debug.LogAssertion("No original set. Cannot copy null object.");
            return;
        }

        // not the original
        if(iteration != 0)
        {
            Debug.LogAssertion("Can only copy from the original.");
            return;
        }

        // make iterations
        for(uint i = 1; i <= totalIterations; i++)
        {
            // makes copy.
            Replicator copy = Instantiate(original);
            copy.iteration = i; // marks iteraton.

            // the direction
            Vector3 direc = GetDirection();

            // spaces out the copy.
            copy.transform.position += direc.normalized * spacing * i;

            // translates the copy by the offset.
            copy.transform.Translate(offset * i);

            // if the parent is the parent.
            if (originalAsParent)
                copy.transform.parent = original.transform;
        }
    }
}
