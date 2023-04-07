using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wandering behaviour for object.
public class WanderBehaviour : SteeringBehaviour
{
    // the radus of the wander circle
    public float radius = 25.0F;

    // position of the sphere
    private Vector3 spherePos;

    // the target's local position (relative to sphere).
    private Vector3 targetPos;

    // if 'true', these axis values are randomized. If false, these values are kept the same as the gameObject.
    public bool randX = true;
    public bool randY = true;
    public bool randZ = true;

    // the amount of time the entity moves in the same direction.
    // when this amount of time has passed (using deltaTime), direction changes.
    // this is done in seconds.
    public float constCourseLength = 10.0F;

    // elapsed course time
    private float elapsedCourseTime = 0.0F;

    // if set to true, the target instantly changes 
    public bool instantCourseChange = false;

    // the factor for how fast the course changes, i.e. target repositions itself.
    // if it is set to 0 or less, it is not applied.
    public float courseChangeSpeed = 1.0F;

    // the target lerp value (0 - 1)
    private float targetLerpT = 0.0F;

    // position 0 and 1 for lerping.
    private Vector3 targetLerpP0;
    private Vector3 targetLerpP1;

    // Start is called before the first frame update
    protected override void Start()
    {
        // calls base.
        base.Start();
        
        // absolute radius
        radius = Mathf.Abs(radius);

        // sphere position
        spherePos = transform.position + transform.forward.normalized * radius;
        
        // target position
        targetPos = spherePos + new Vector3(
            Random.Range(-radius, radius),
            Random.Range(-radius, radius),
            Random.Range(-radius, radius)
            );

        // save both target positions.
        targetLerpP0 = targetPos;
        targetLerpP1 = targetPos;

        // change direction on start
        elapsedCourseTime = constCourseLength;
    }

    // rotates a vector2 (uses degrees)
    private Vector2 RotateVector2(Vector2 v, float a, bool inDegrees = true)
    {
        float theta = (inDegrees) ? a : a * Mathf.Rad2Deg;

        return new Vector2(
            v.x * Mathf.Cos(theta) - v.y * Mathf.Sin(theta),
            v.x * Mathf.Sin(theta) + v.y * Mathf.Cos(theta));
    }

    // returns the sphere position
    public Vector3 GetSpherePosition()
    {
        return spherePos;
    }

    // returns the target position.
    public Vector3 GetTargetPosition()
    {
        return targetPos;
    }

    // checks to see if the behaviour should be updated.
    public override bool UpdateAvailable()
    {
        return true;
    }

    // updates the behaviour
    public override void RunBehaviour()
    {
        // calculates sphere position relative to player
        spherePos = transform.position + transform.forward.normalized * radius;

        // the entity has gone in the same direction long enough, so change direction.
        if(elapsedCourseTime >= constCourseLength)
        {
            // randomizes the local target position within the sphere. 
            // local target position
            Vector3 localTargetPos = new Vector3(
                Random.Range(-radius, radius),
                Random.Range(-radius, radius),
                Random.Range(-radius, radius)
                );

            // save old position.
            targetLerpP0 = targetPos;

            // save new position.
            // gets new target position from local sphere position.
            targetLerpP1 = spherePos + localTargetPos;

            // new lerp to take place.
            targetLerpT = 0.0F;

            // snap to new position instantly.
            if (instantCourseChange)
            {
                targetPos = targetLerpP1;
            }
            else // gradually change course.
            {
                targetLerpT += Time.deltaTime * courseChangeSpeed;
                targetPos = Vector3.Lerp(targetLerpP0, targetLerpP1, targetLerpT); // automatically clamps
            }
                

            // start timer over.
            elapsedCourseTime = 0.0F;
        }
        else // keep going the same direction
        {
            elapsedCourseTime += Time.deltaTime;

            // if the course change isn't instant.
            if(!instantCourseChange)
            {
                targetLerpT += Time.deltaTime * courseChangeSpeed;
                targetLerpT = Mathf.Clamp01(targetLerpT); // clamps value
                targetPos = Vector3.Lerp(targetLerpP0, targetLerpP1, targetLerpT); // updates position
            }            
        }

        // clamp target position.
        targetPos.x = Mathf.Clamp(targetPos.x, spherePos.x - radius, spherePos.x + radius);
        targetPos.y = Mathf.Clamp(targetPos.y, spherePos.y - radius, spherePos.y + radius);
        targetPos.z = Mathf.Clamp(targetPos.z, spherePos.z - radius, spherePos.z + radius);

        // if the x-value should not be randomized.
        if (!randX)
            targetPos.x = transform.position.x;

        // if the y-value should not be randomized.
        if (!randY)
            targetPos.y = transform.position.y;

        // if the z-value should not be randomized.
        if (!randZ)
            targetPos.z = transform.position.z;

        // look at the target
        transform.LookAt(targetPos);

        // move in direction
        rigidBody.AddForce(transform.forward * speed * Time.deltaTime, forceMode);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // calls update. If set to do so, this will automatically call 'UpdateBehaviour'.
        base.Update();
    }
}
