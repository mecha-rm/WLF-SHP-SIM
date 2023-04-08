using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the wolf object.
public class Wolf : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // species
        species = "Wolf";

        // entity name has not been set.
        if (entityName == "")
            entityName = "Wolf";

        // the description has not been set.
        if (description == "")
            description = "Predatory species that kills sheep.";

        base.Start();
    }

    //// called when the sheep is colliding with something.
    //private void OnTriggerStay(Collider other)
    //{
    //    // if the object is grass.
    //    if (other.gameObject.tag == "Sheep" && IsHungry())
    //    {
    //        // grabs component.
    //        Sheep sheep = other.gameObject.GetComponent<Sheep>();

    //        // component found.
    //        if (sheep != null)
    //        {
    //            // eat the sheep.
    //            EatSheep(sheep);
    //        }
    //    }
    //}

    // Called when the trigger exits.
    private void OnTriggerExit(Collider other)
    {
        // Checks if it's a sheep.
        Sheep sheep;

        // If the other object is a sheep, stop pursing it since it's out of range.
        if (other.gameObject.TryGetComponent(out sheep))
        {
            // Checks if a food is already set or not.
            if (threat == sheep)
            {
                // Clear food.
                SetFood(null);
            }
        }
    }

    // Called when an entity collision has happened.
    public override void OnEntityCollision(Entity entity, bool isTrigger)
    {
        // Checks what entity has been collided with.
        if (entity is Sheep)
        {
            // If the wolf is hungry...
            if (IsHungry())
            {
                // Checks if it's the trigger collision or not.
                if(isTrigger) // Make target.
                {
                    // If food isn't set, set the targeted object.
                    if(food == null)
                    {
                        // TODO: maybe set the velocity to 0 so that the wolf doesn't skid? Or use arrive?
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

                    // Eat the sheep.
                    EatSheep((Sheep)entity);
                }

            }
        }
    }

    // eats sheep.

    public void EatSheep(Sheep sheep)
    {
        // adds to nourishment value.
        // TODO: this value should not be hardcoded.
        nourishedValue += nourishedEatInc;

        // recalculate hunger value.
        CalculateHunger();

        // kills sheep
        sheep.Kill();
    }

    // kills the wolf.
    public override void Kill()
    {
        EntityManager.GetInstance().ReturnEntity(this);
    }

    // reproduces the wolf.
    protected override void Reproduce()
    {
        Wolf wolf = EntityManager.GetInstance().GetWolf();
        wolf.transform.position = transform.position;

        // TODO: change position.
        wolf.transform.position = transform.position + new Vector3(0.0F, 1.0F, 0.0F);
    }

    // wolf has been killed.
    public override void OnDeath(GameObject killer)
    {
        // returns the wolf to the pool.
        EntityManager.GetInstance().ReturnEntity(this);
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
            
    }
}
