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

    // If set to 'true', deltaTime is applied to the speed of the object.
    public bool applyDeltaTime = true;

    // If set to 'true', the behaviour is run.
    public bool runBehaviour = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // finds the rigidbody
        if (rigidBody == null)
            rigidBody = gameObject.GetComponent<Rigidbody>();

        // adds rigidbody to item
        if (rigidBody == null)
            gameObject.AddComponent<Rigidbody>();
    }

    // Zeroes out the velocity on the rigid body.
    public void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }

    // Applies force to the attached object. The vector provided is the distance of the force.
    protected void ApplyForce(Vector3 direction)
    {
        ApplyForce(direction, speed, forceMode, false);
    }

    // Applies force with the provided speed. Argument 'overwrite' determines if this should be set as the object's speed.
    protected void ApplyForce(Vector3 direction, float a_speed, bool overwrite)
    {
        ApplyForce(direction, a_speed, forceMode, overwrite);
    }

    // Applies force with the provided force mode.
    // Argument 'overwrite' determines if this should be set as the object's force mode.
    protected void ApplyForce(Vector3 direction, ForceMode a_forceMode, bool overwrite)
    {
        ApplyForce(direction, speed, a_forceMode, overwrite);
    }

    // Applies force to the attached object. The vector provided is the distance of the force.
    // If 'overwrite' is set to 'true', then the set speed and force mode are overwritten.
    protected void ApplyForce(Vector3 direction, float a_speed, ForceMode a_forceMode, bool overwrite)
    {
        // Set forward to the normalized distance vector.
        transform.forward = direction.normalized;

        // Calculates the force that's being applied.
        Vector3 force = transform.forward * a_speed;

        // Applies delta time to the object's force.
        if (applyDeltaTime)
            force *= Time.deltaTime;


        // Adds force to the rigidbody.
        rigidBody.AddForce(force, a_forceMode);

        // Values should be overwritten.
        if (overwrite)
        {
            speed = a_speed;
            forceMode = a_forceMode;
        }
    }

    // if this returns 'true', a behaviour update is available.
    // if this returns 'false', there is no update available.

    public abstract bool UpdateAvailable();

    // Runs the entitiy's behaviour.
    public abstract void RunBehaviour();

    // Update is called once per frame
    protected virtual void Update()
    {
        // If the behaviour shouldn't be updated.
        if (runBehaviour)
        {
            RunBehaviour();
        }
    }
}
