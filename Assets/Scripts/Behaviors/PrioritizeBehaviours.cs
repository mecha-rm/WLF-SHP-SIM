using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// behaviour tree take makes some behaviours override others.
public class PrioritizeBehaviours : MonoBehaviour
{
    // the list of possible behaviours
    // if a behaviour is added to this list, make sure to turn off it's automatic updates.
    public List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

    // the behaviour priorities.
    // the lower the number, the higher the priority.
    // if a behaviour's priority is less than 0, then it will never be called.
    // if priorities aren't set, then the location in the list is used.
    public List<int> priorities = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        // no behaviours set.
        if(behaviours.Count == 0)
        {
            // gets behaviours and stores them in the list.
            GetComponents<SteeringBehaviour>(behaviours);
        }

        // if no priorities are set, match the priorities to the indexes.
        if(priorities.Count == 0)
            MatchPrioritiesToIndexes(); // add priorities.
    }

    // match the priorities 
    public void MatchPrioritiesToIndexes()
    {
        // goes through each behaviour
        for(int i = 0; i < behaviours.Count; i++)
        {
            // no priority set.
            if(i >= priorities.Count)
            {
                priorities.Add(i); // match priority to index.
            }
            else
            {
                // set priority to place in list.
                priorities[i] = i;
            }
        }
    }

    // clears all lists
    public void ClearLists()
    {
        behaviours.Clear();
        priorities.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        // the priority of the actions being used.
        int actionPriority = -1;

        // queue of actions to be used.
        Queue<SteeringBehaviour> actions = new Queue<SteeringBehaviour>();

        // goes through all behaviours
        for (int i = 0; i < behaviours.Count; i++)
        {
            // gets the current behaviour
            SteeringBehaviour behaviour = behaviours[i];

            // the behaviour can be updated, and said behaviour is active.
            if(behaviour.UpdateAvailable() && behaviour.activeBehaviour)
            {
                // priority of current behaviour.
                int p = 0;

                // if there is a priority to grab.
                if (i < priorities.Count)
                {
                    // if the priority is negative, set it to 0.
                    if (priorities[i] < 0)
                        priorities[i] = 0;
                    
                    // save priority value.
                    p = priorities[i];
                }
                else // adds priority, and makes it set to the current index.
                {
                    p = 0;
                    priorities.Add(i); // priority is set to current index.
                }

                // if the action priority is negative, it means no action has been available yet.
                if(actionPriority < 0)
                {
                    actionPriority = p;
                }

                // this is of the right priority.
                if(actionPriority == p)
                {
                    // adds to list of actions.
                    actions.Enqueue(behaviour);
                }
            }
        }

        // while there are still actions to activate.
        while (actions.Count > 0)
        {
            SteeringBehaviour sb = actions.Peek(); // get front.
            actions.Dequeue(); // remove front.
            sb.UpdateBehaviour(); // updates behaviour

        }
    }
}
