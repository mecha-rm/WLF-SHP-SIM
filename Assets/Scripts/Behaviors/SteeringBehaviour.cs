/*
 * Resources:
 *  - https://www.red3d.com/cwr/steer/gdc99/
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AI of the object.
public abstract class SteeringBehaviour : MonoBehaviour
{
    // the rigidboy of the object
    public Rigidbody rigidBody;

    // force mode for the AI
    public ForceMode forceMode = ForceMode.Acceleration;

    // speed of object
    public float speed = 500.0F;

    // updates the behaviour
    public bool autoUpdateBehaviour = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // finds the rigidbody
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();

        // adds rigidbody to item
        if (rigidBody == null)
            gameObject.AddComponent<Rigidbody>();
    }

    // if this returns 'true', a behaviour update is available.
    // if this returns 'false', there is no update available.

    public abstract bool UpdateAvailable();

    public abstract void UpdateBehaviour();

    // Update is called once per frame
    protected void Update()
    {
        // if the behaviour shouldn't be updated.
        if (autoUpdateBehaviour)
        {
            UpdateBehaviour();
        }
    }
}
