using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the sheep object.
public class Sheep : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // if the speices has not been set.
        if(species == "")
            species = "sheep";

        // entity name has not been set.
        if (entityName == "")
            entityName = "Sheep";

        // the description has not been set.
        if (description == "")
            description = "Prey species that feeds on grass.";

        base.Start();

    }

    //// called when the sheep is colliding with something.
    //private void OnTriggerStay(Collider other)
    //{
    //    // if the object is grass.
    //    if(other.gameObject.tag == "Grass" && IsHungry())
    //    {
    //        // grabs component.
    //        Grass grass = other.gameObject.GetComponent<Grass>();

    //        // component found.
    //        if(grass != null)
    //        {
    //            // checks if the grass is fully edible.
    //            if(grass.IsEdible())
    //            {
    //                // eats the grass.
    //                EatGrass(grass);
    //            }
    //        }
    //    }
    //}

    // Called when the trigger exits.
    private void OnTriggerExit(Collider other)
    {
        // Checks if it's a wolf.
        Wolf wolf;

        // If the other object is a wolf, stop running from it.
        if (other.gameObject.TryGetComponent(out wolf))
        {
            // Checks if a threat is already set or not.
            if(threat == wolf)
            {
                // Clear threat.
                SetThreat(null);
            }
        }
    }

    // Called when an entity collision has happened.
    public override void OnEntityCollision(Entity entity, bool isTrigger)
    {
        // Checks if the entity is grass.
        if (entity is Grass)
        {
            // If the wolf is hungry...
            if (IsHungry())
            {
                // Checks if it's the trigger collision or not.
                if (isTrigger) // Make target.
                {
                    // If food isn't set, set the targeted object.
                    if (food == null)
                    {
                        SetFood(entity.gameObject);
                    }
                    else // Check which one is closer.
                    {
                        // If the new entity is the closest one, go for it.
                        if (ClosestObject(gameObject, food, entity.gameObject) == entity.gameObject)
                            SetFood(entity.gameObject);
                    }

                }
                else // Eat
                {
                    // Food object emptied.
                    SetFood(null);

                    // Eat the grass.
                    EatGrass((Grass)entity);
                }

            }
        }
        else if(entity is Wolf)
        {
            // Retreat from the wolf.
            SetThreat(entity.gameObject);
        }
    }

    // eats grass.

    public void EatGrass(Grass grass)
    {
        // adds to nourishment value.
        // TODO: this value should not be hardcoded.
        nourishedValue += nourishedEatInc;

        // recalculate hunger value.
        CalculateHunger();

        // tells grass its been eaten.
        grass.Eaten();
    }

    // kills the sheep.
    public override void Kill()
    {
        EntityManager.GetInstance().ReturnEntity(this);
    }

    // reproduces the sheep.
    protected override void Reproduce()
    {
        Sheep sheep = EntityManager.GetInstance().GetSheep();
        sheep.transform.position = transform.position;

        // TODO: change position.
        sheep.transform.position = transform.position + new Vector3(0.0F, 1.0F, 0.0F);
    }

    // sheep has been killed.
    public override void OnDeath(GameObject killer)
    {
        // returns the sheep to the pool.
        EntityManager.GetInstance().ReturnEntity(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
