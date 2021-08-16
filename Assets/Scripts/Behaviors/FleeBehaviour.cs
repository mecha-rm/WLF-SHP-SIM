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

    // potential threat is in the view
    private void OnTriggerStay(Collider other)
    {
        // sets new threat.
        if (threat == null && threatTags.Contains(other.tag))
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

    // checks to see if the behaviour should be updated.
    public override bool UpdateAvailable()
    {
        // if there's a current threat, return true.
        return (threat != null);
    }

    // updates the behaviour
    public override void UpdateBehaviour()
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
    void Update()
    {
        // calls update. If set to do so, this will automatically call 'UpdateBehaviour'.
        base.Update();
    }
}
