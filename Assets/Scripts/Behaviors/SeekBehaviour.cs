using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : AIBehaviour
{
    // view for seeking items
    public SphereCollider view;

    // seek target
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        // if the view is set to null
        if(view == null)
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

    // potential target is in the view
    private void OnTriggerStay(Collider other)
    {
        // sets new target.
        if (target == null)
        {
            target = other.gameObject;
        }
            
    }

    // target has left range.
    private void OnTriggerExit(Collider other)
    {
        // target is now set to null.
        if (other.gameObject == target)
            target = null;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: this keeps detecting itself, or the platform below. Make sure to check for proper tags.

        // calls update
        base.Update();

        // target is set.
        if(target != null)
        {
            // gets direction
            Vector3 direc = target.transform.position - gameObject.transform.position;
            direc.Normalize();

            // adds force to rigid body
            rigidBody.AddForce(direc * speed * Time.deltaTime, forceMode);
        }
    }
}
