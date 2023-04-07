using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// flees an entity
public class FleeBehaviour : SteeringBehaviour
{
    // tags of objects recognized as threats
    public List<string> threatTags = new List<string>();

    // view for seeking items
    public SphereCollider view;

    // threat to escape from
    public GameObject threat;

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

    // Potential threat is in the view
    private void OnTriggerStay(Collider other)
    {
        // Gets the current distance and other distance.
        float currDist = (threat != null) ? (threat.transform.position - transform.position).magnitude : -1;
        float otherDist = (other.transform.position - transform.position).magnitude;

        // Checks if a new threat should be set. Checks if the new threat is closer, or if the threat is not set.
        if (otherDist < currDist || threat == null)
        {
            // TODO: check object priority.
            threat = other.gameObject;
        }
    }

    // threat has left range.
    private void OnTriggerExit(Collider other)
    {
        // threat is now set to null.
        if (other.gameObject == threat)
            threat = null;
    }

    // checks to see if the behaviour should be updated.
    public override bool UpdateAvailable()
    {
        // if there's a current threat, return true.
        return (threat != null);
    }

    // updates the behaviour
    public override void RunBehaviour()
    {
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

    // Update is called once per frame
    protected override void Update()
    {
        // calls update. If set to do so, this will automatically call 'UpdateBehaviour'.
        base.Update();
    }
}
