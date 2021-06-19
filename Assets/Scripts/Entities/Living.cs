using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for living entities
public abstract class Living : Entity
{
    // the species of the living entity.
    protected string species = "";


    // lifespan and age (in years)
    /*
     * Lifespan is the maximum amount of years something can live.
     *  - each child (i.e. species) has a set life span, which is the maximum amount of time a given entity can stay alive.
     * Life expectacny is the expected amount of time something can live.
     *  - the life expentancy fluctates based on various environmental factors.
     */
    // life span is static, and set by each derived class as part of a pure virtual function.
    // life expectancy.
    private int lifeExpect = 0;

    // age (in years)
    private int age = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // returns the life expectancy
    public abstract int GetLifeSpan();

    // gets the life expectancy
    public int LifeSpan
    {
        get { return GetLifeSpan(); } 
    }

    // gets the life expectancy
    public int GetLifeExpectancy()
    {
        return lifeExpect;
    }

    // sets the life expectancy. It cannot be negative.
    protected void SetLifeExpectancy(int newLifeExpect)
    {
        lifeExpect = (newLifeExpect >= 0) ? newLifeExpect : lifeExpect;
    }


    // age getter/setter
    // getter
    public int GetAge()
    {
        return age;
    }

    // sets the current age. This number cannot be negative.
    protected void SetAge(int newAge)
    {
        age = (newAge >= 0) ? newAge : age;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
