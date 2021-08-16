using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// seeks an behaviour
public class SeekBehaviour : SteeringBehaviour
{
    // tags of objects recognized as threats
    public List<string> targetTags = new List<string>();

    // view for seeking items
    public SphereCollider view;

    // seek target
    public GameObject target;

    // Start is called before the first frame update
    protected override void Start()
    {
        // calls base.
        base.Start();

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

    // potential target is in the view
    private void OnTriggerStay(Collider other)
    {
        // sets new target.
        if (target == null && targetTags.Contains(other.tag))
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

    // checks to see if the behaviour should be updated.
    public override bool UpdateAvailable()
    {
        // if currently seeking a target, update.
        return (target != null);
    }

    // updates the behaviour
    public override void UpdateBehaviour()
    {
        // target is set.
        if (target != null)
        {
            // gets direction
            Vector3 direc = target.transform.position - gameObject.transform.position;
            direc.Normalize();

            // adds force to rigid body
            rigidBody.AddForce(direc * speed * Time.deltaTime, forceMode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // calls update. If set to do so, this will automatically call 'UpdateBehaviour'.
        base.Update();
    }
}
