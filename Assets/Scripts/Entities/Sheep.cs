using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the sheep object.
public class Sheep : Animal
{
    // Start is called before the first frame update
    void Start()
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

    }

    // called when the sheep is colliding with something.
    private void OnTriggerStay(Collider other)
    {
        // if the object is grass.
        if(other.gameObject.tag == "Grass" && IsHungry())
        {
            // grabs component.
            Grass grass = other.gameObject.GetComponent<Grass>();

            // component found.
            if(grass != null)
            {
                // checks if the grass is fully edible.
                if(grass.IsEdible())
                {
                    // eats the grass.
                    EatGrass(grass);
                }
            }
        }
    }

    // eats grass.
   
    public void EatGrass(Grass grass)
    {
        // adds to nourishment value.
        // TODO: this value should not be hardcoded.
        nourishedValue += 10.0F;

        // checks for hunger.
        IsHungry();

        // tells grass its been eaten.
        grass.Eaten();
    }
    
    // calculates the hunger
    public void CalculateHungry()
    {
        // returned value.
        bool hungry = false;

        // caps value.
        nourishedValue = Mathf.Clamp(nourishedValue, 0.0F, nourishedMax);

        // checks result.
        hungry = !(nourishedValue < fullThreshold);

        // checks if hungry. If so, start looking for food. If not, stop.
        if (hungry && foodSeek != null)
            foodSeek.activeBehaviour = true;
        else if (!hungry && foodSeek != null)
            foodSeek.activeBehaviour = false;
    }

    // checks to see if the sheep is hungry.
    public bool IsHungry()
    {
        return nourishedValue < fullThreshold;
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
        // throw new System.NotImplementedException();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
