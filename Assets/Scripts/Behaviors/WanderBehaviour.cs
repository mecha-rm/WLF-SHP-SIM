using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wandering behaviour for object.
public class WanderBehaviour : MonoBehaviour
{
    // movement speed
    public float speed = 25.0F;

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
        radius = Mathf.Abs(radius);
        angleZero = new Vector2(radius, 0.0F);
        targetPos = angleZero;
    }

    // rotates a vector2
    private Vector2 RotateVector2(Vector2 v, float a)
    {
        return new Vector2(
            v.x * Mathf.Cos(a) - v.y * Mathf.Sin(a),
            v.x * Mathf.Sin(a) + v.y * Mathf.Cos(a));
    }

    // Update is called once per frame
    void Update()
    {
        // calculates circle position
        Vector3 circPos = transform.position;
        circPos += transform.forward.normalized * radius;

        // TODO: calculate new target angle from center
        


        // transform.Translate(speed);
        // Vector3.MoveTowards;
    }
}
