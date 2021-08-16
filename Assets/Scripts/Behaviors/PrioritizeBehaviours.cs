using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// behaviour tree take makes some behaviours override others.
public class PrioritizeBehaviours : MonoBehaviour
{
    // the list of possible behaviours
    // if a behaviour is added to this list, make sure to turn off it's automatic updates.
    public List<SteeringBehaviour> behaviours;

    // the behaviour priorities.
    // the lower the number, the higher the priority.
    // if a behaviour's priority is less than 0, then it will never be called.
    public List<int> priorities;

    // Start is called before the first frame update
    void Start()
    {
        
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

            // the behaviour can be updated.
            if(behaviour.UpdateAvailable())
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
                else // adds priority, and makes it 0.
                {
                    p = 0;
                    priorities.Add(0);
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

            // while there are still actions to activate.
            while(actions.Count >= 0)
            {
                SteeringBehaviour sb = actions.Peek(); // get front.
                actions.Dequeue(); // remove front.
                sb.UpdateBehaviour(); // updates behaviour

            }
        }
    }
}
