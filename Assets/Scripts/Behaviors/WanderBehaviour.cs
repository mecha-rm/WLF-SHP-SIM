using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wandering behaviour for object.
public class WanderBehaviour : AIBehaviour
{
    // the radus of the wander circle
    public float radius = 25.0F;

    // the target's local position.
    private Vector2 targetPos;

    // the angle of the circle's target.
    private float targetAngle = 0.0F;

    // the zero angle of the wandering behaviour
    private Vector2 angleZero;

    // Start is called before the first frame update
    void Start()
    {
        // grabs the rigid body
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();

        // finds the rigid body in the children.
        if (rigidBody == null)
            rigidBody = GetComponentInChildren<Rigidbody>();

        radius = Mathf.Abs(radius);
        angleZero = new Vector2(radius, 0.0F);
        targetPos = angleZero;
    }

    // rotates a vector2 (uses degrees)
    private Vector2 RotateVector2(Vector2 v, float a, bool inDegrees = true)
    {
        float theta = (inDegrees) ? a : a * Mathf.Rad2Deg;

        return new Vector2(
            v.x * Mathf.Cos(theta) - v.y * Mathf.Sin(theta),
            v.x * Mathf.Sin(theta) + v.y * Mathf.Cos(theta));
    }

    // updates the behaviour
    void UpdateBehaviour()
    {
        // calculates circle position
        Vector3 circPos = transform.position;
        circPos += transform.forward.normalized * radius;

        // TODO: calculate new target angle from center
        // targetAngle += 1.0F;

        Vector3 targetLocal = RotateVector2(angleZero, targetAngle);
        targetPos = circPos + targetLocal; // sets new target pos.

        transform.position = Vector3.RotateTowards(transform.position, targetPos, 6.0F, 0.0F);
        transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.y));

        // adds force
        // if (rigidBody != null)
        //     rigidBody.AddForce(transform.forward.normalized * speed * Time.deltaTime);

        // uses translate
        transform.Translate(transform.forward.normalized * speed * Time.deltaTime);

        // transform.Rotate(Vector3.up, targetAngle);
    }

    // Update is called once per frame
    void Update()
    {
        // calls update
        base.Update();

        // updates hte behaviour
        UpdateBehaviour();
    }
}
