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
    }

    // called when the sheep is colliding with something.
    private void OnTriggerStay(Collider other)
    {
        // if the object is grass.
        if (other.gameObject.tag == "Sheep" && IsHungry())
        {
            // grabs component.
            Sheep sheep = other.gameObject.GetComponent<Sheep>();

            // component found.
            if (sheep != null)
            {
                // eat the sheep.
                EatSheep(sheep);
            }
        }
    }

    // TODO: use to eat sheep.
    public override bool Eat()
    {
        throw new System.NotImplementedException();
    }

    // eats sheep.

    public void EatSheep(Sheep sheep)
    {
        // adds to nourishment value.
        // TODO: this value should not be hardcoded.
        nourishedValue += 10.0F;

        // recalculate hunger value.
        CalculateHunger();

        // kills sheep
        sheep.Kill();
    }

    // kills the wolf.
    public override void Kill()
    {
        EntityManager.GetInstance().ReturnWolf(this);
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
        EntityManager.GetInstance().ReturnWolf(this);
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
            
    }
}
