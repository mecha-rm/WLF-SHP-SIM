using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: the sphere collider and the box collider trigger the same thing.
// it's impossible to know which collider triggred what.
// as such, the 'eat' collider and the 'behaviour' collider interfere with one another.
// the behaviour collider is bigger, so it will win out everytime.
// if continued, this needs to be fixed somehow.

// animal behaviour
public abstract class Animal : Entity
{
    // the species of the living entity.
    protected string species = "";

    // sex of the living thing
    public enum sex { unknown, male, female }

    // the variables concerning animal life times.
    [Header("Life Time")]

    // lifespan and age
    /*
     * Lifespan is the maximum amount of years something can live.
     *  - each child (i.e. species) has a set life span, which is the maximum amount of time a given entity can stay alive.
     * Life expectancy is the expected amount of time something can live.
     *  - the life expentancy fluctates based on various environmental factors.
     */

    // the life span (in seconds)
    // this is the maximum amount of time an animal can lie for.
    public float lifeSpan = 10.0F;

    // life expectancy (in seconds) - cannot suppass life span.
    // this is the animal's death date, which is lifeSpan adjusted for by other factors.
    private float lifeExpect = 10.0F;

    // age (in years)
    public float age = 0;

    // if 'true', the entity is aging.
    public bool aging = true;

    // timer for giving birth.
    public float birthTimer = 0.0F;

    // maximum amount of time for giving birth.
    public float birthTimeMax = 5.0F;

    [Header("Eating")]

    // behaviour for seeking food.
    public SeekBehaviour foodSeek;

    // the value to see how well nourished the animal is.
    public float nourishedValue = 0.0F;

    // the threshold that must passed for the entity to stop eating. 
    public float fullThreshold = 80.0F;

    // the value when the sheep is considered 'full'.
    public float nourishedMax = 100.0F;

    // animal conditions
    [Header("Conditions")]

    // if set to 'true', the living entity is sick.
    // how this behaviour is defined depends on the entity.
    public bool sick = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // gets the life span
    public float GetLifeSpan()
    {
        return lifeSpan;
    }

    // sets the life span
    public void SetLifeSpan(float newLifeSpan)
    {
        lifeSpan = newLifeSpan;
    }


    // returns the life expectancy
    public float GetLifeExpectancy()
    {
        return lifeExpect;
    }

    // sets the life expectancy. It cannot be negative.
    protected void SetLifeExpectancy(float newLifeExpect)
    {
        lifeExpect = (newLifeExpect >= 0.0F) ? newLifeExpect : lifeExpect;
    }

    // gets the age
    public float GetAge()
    {
        return age;
    }

    // sets the current age. This number cannot be negative.
    protected void SetAge(float newAge)
    {
        age = (newAge >= 0.0F) ? newAge : age;
    }

    // calculates the hunger
    public void CalculateHunger()
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

    // used to make the animal reporduce.
    protected abstract void Reproduce();

    // kills the animal.
    public abstract void Kill();

    // called when a living entity dies, and passes it its killer.
    // if the entity itself is passed, then the entity died of natural causes.
    // if null is passed, then it is unknown what killed the entity (possibly forcibly terminated).
    public abstract void OnDeath(GameObject killer);


    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        // increases age.
        if (aging)
            age += Time.deltaTime;

        // for giving birth.
        {
            // increments birth timer.
            birthTimer += Time.deltaTime;

            // gives birth.
            if (birthTimer > 5.0F)
            {
                Reproduce();
                birthTimer = 0.0F;
            }
        }

        // for nourishment.
        if(nourishedValue > 0.0F)
        {
            // reduces nourishment value.
            nourishedValue -= Time.deltaTime;

            // bounds check.
            if (nourishedValue < 0.0F)
                nourishedValue = 0.0F;
        }

        // if the animal should be hungry.
        if (nourishedValue < fullThreshold && foodSeek != null)
        {
            // look for food.
            foodSeek.activeBehaviour = true;
        }
        else if (nourishedValue >= fullThreshold && foodSeek != null)
        {
            // not looking for food.
            foodSeek.activeBehaviour = false;
        }

        // checking for death
        // TODO: adjust life expectancy based on different factors.
        {
            lifeExpect = lifeSpan;
        }
        

        // if the age has reached the life expectancy or life span
        if (age >= lifeExpect || age >= lifeSpan)
            OnDeath(gameObject); // has been killed.
    }

    // on destroy
    private void OnDestroy()
    {

    }
}
