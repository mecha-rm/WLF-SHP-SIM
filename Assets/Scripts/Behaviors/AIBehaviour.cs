using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AI of the object.
public class AIBehaviour : MonoBehaviour
{
    // the rigidboy of the object
    public Rigidbody rigidBody;

    // force mode for the AI
    public ForceMode forceMode = ForceMode.Acceleration;

    // speed of object
    public float speed = 25.0F;

    // updates the behaviour
    public bool updateBehaviour = true;

    // Start is called before the first frame update
    void Start()
    {
        // finds the rigidbody
        if (rigidBody == null)
            rigidBody = FindObjectOfType<Rigidbody>();
    }

    // Update is called once per frame
    protected void Update()
    {
        // if the rigidbody does not exist.
        if (rigidBody == null)
        {
            Debug.LogError("No rigid body set. Unable to apply behaviour.");
            return;
        }

        // if the behaviour shouldn't be updated.
        if (!updateBehaviour)
            return;
    }
}
