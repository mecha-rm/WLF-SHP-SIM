using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for living entities
public abstract class Living : Entity
{
    // Living variables
    [Header("Living")]

    // the species of the living entity.
    protected string species = "";

    // lifespan and age
    /*
     * Lifespan is the maximum amount of years something can live.
     *  - each child (i.e. species) has a set life span, which is the maximum amount of time a given entity can stay alive.
     * Life expectancy is the expected amount of time something can live.
     *  - the life expentancy fluctates based on various environmental factors.
     */

    // the life span (in seconds)
    public float lifeSpan = 10.0F;

    // life expectancy (in seconds) - cannot suppass life span.
    public float lifeExpect = 10.0F;
    
    // age (in years)
    public float age = 0;

    // if 'true', the entity is aging.
    public bool aging = true;

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

    // called when a living entity kills another entity, and passes in its victim.
    public abstract void Kills(GameObject victim);

    // called when a living entity dies, and passes it its killer.
    // if the entity itself is passed, then the entity died of natural causes.
    // if null is passed, then it is unknown what killed the entity (possibly forcibly terminated).
    public abstract void OnKilled(GameObject killer);



    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        // increases age.
        if(aging)
            age += Time.deltaTime;

        // if the age has reached the life expectancy or life span
        if (age >= lifeExpect || age >= lifeSpan)
            OnKilled(gameObject); // has been killed.
    }

    // on destroy
    private void OnDestroy()
    {
        
    }
}
