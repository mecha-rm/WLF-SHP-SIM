using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// flees an entity
public class FleeBehaviour : AIBehaviour
{
    // view for seeking items
    public SphereCollider view;

    // threat to escape from
    public GameObject threat;

    // Start is called before the first frame update
    void Start()
    {
        // if the view is set to null
        if (view == null)
        {
            // adds view component
            view = gameObject.AddComponent<SphereCollider>();
            view.isTrigger = true;
        }
        else if (view != null)
        {
            // if the view wasn't set to be a trigger, it is now.
            if (view.isTrigger == false)
                view.isTrigger = true;
        }

    }

    // potential threat is in the view
    private void OnTriggerStay(Collider other)
    {
        // sets new threat.
        if (threat == null)
        {
            threat = other.gameObject;
        }

    }

    // has escaped threat
    private void OnTriggerExit(Collider other)
    {
        // threat is now set to null.
        if (other.gameObject == threat)
            threat = null;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: this keeps detecting itself, or the platform below. Make sure to check for proper tags.

        // calls update
        base.Update();

        // target is set.
        if (threat != null)
        {
            // gets direction
            Vector3 direc = gameObject.transform.position - threat.transform.position;
            direc.Normalize();

            // adds force to rigid body
            rigidBody.AddForce(direc * speed * Time.deltaTime, forceMode);
        }
    }
}
