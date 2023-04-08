using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using util;
using static UnityEngine.GraphicsBuffer;

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

    // The tags for checking collided objects.
    const string GRASS_TAG = "Grass";
    const string SHEEP_TAG = "Sheep";
    const string WOLF_TAG = "Wolf";
    
    // TODO: one idea is to have the species of the same type space out when they aren't hungry or looking to breed.
    // This might make the behaviours better.

    // sex of the living thing
    public enum sex { unknown, male, female }

    // Behaviours
    public SeekBehaviour seek;
    public PursueBehaviour pursue; // TODO: implement
    public FleeBehaviour flee;
    public EvadeBehaviour evade; // TODO: implement
    public WanderBehaviour wander;

    // These variables are used to prioritize different actions.
    protected int foodPriority = 3; // Prioritize eating.
    protected GameObject food; // Target to go towards.

    protected int threatPriority = 2; // Prioritize fleeing.
    protected GameObject threat; // Target to flee from.

    protected int wanderPriority = 1; // Prioritize wandering.

    // The entity's speed.
    public float speed = 5.0F;

    // The force mode for the entity.
    public ForceMode forceMode = ForceMode.Force;

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
    public float lifeSpan = 800.0F;

    // life expectancy (in seconds) - cannot suppass life span.
    // this is the animal's death date, which is lifeSpan adjusted for by other factors.
    private float lifeExpect = 300.0F;

    // age (in years)
    public float age = 0;

    // if 'true', the entity is aging.
    public bool aging = true;

    // timer for giving birth.
    public float birthTimer = 0.0F;

    // maximum amount of time for giving birth.
    public float birthTimeMax = 75.0F;

    [Header("Eating")]

    // the value to see how well nourished the animal is.
    public float nourishedValue = 0.0F;

    // increment for eating.
    public float nourishedEatInc = 5.0F;

    // the threshold that must passed for the entity to stop eating. 
    public float fullThreshold = 15.0F;

    // the value when the sheep is considered 'full'.
    public float nourishedMax = 20.0F;

    // animal conditions
    [Header("Conditions")]

    // if set to 'true', the living entity is sick.
    // how this behaviour is defined depends on the entity.
    public bool sick = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Checks for seek behaviour.
        if(seek == null)
        {
            if (!TryGetComponent(out seek))
                seek = gameObject.AddComponent<SeekBehaviour>();
        }

        // Checks for pursue behaviour.
        if (pursue == null)
        {
            if (!TryGetComponent(out pursue))
                pursue = gameObject.AddComponent<PursueBehaviour>();
        }

        // Checks for flee behaviour.
        if (flee == null)
        {
            if (!TryGetComponent(out flee))
                flee = gameObject.AddComponent<FleeBehaviour>();
        }

        // Checks for evade behaviour.
        if (evade == null)
        {
            if (!TryGetComponent(out evade))
                evade = gameObject.AddComponent<EvadeBehaviour>();
        }

        // Checks for wander behaviour.
        if (wander == null)
        {
            if (!TryGetComponent(out wander))
                wander = gameObject.AddComponent<WanderBehaviour>();
        }

        // Disables auto-running for behaviours, and sets the speed of each one.
        seek.runBehaviour = false;
        seek.speed = speed;
        seek.forceMode = forceMode;

        pursue.runBehaviour = false;
        pursue.speed = speed;
        pursue.forceMode = forceMode;

        flee.runBehaviour = false;
        flee.speed = speed;
        flee.forceMode = forceMode;

        evade.runBehaviour = false;
        evade.speed = speed;
        evade.forceMode = forceMode;

        wander.runBehaviour = false;
        wander.speed = speed;
        wander.forceMode = forceMode;
    }

    // Called when the collision stay. This is used for eating an entity.
    // The sheep needs this to be a collision stay.
    private void OnCollisionStay(Collision collision)
    {
        // Entity object.
        Entity entity;

        // Checks if the entity is an entity object. If it is, call the entity collision object.
        if (collision.gameObject.TryGetComponent(out entity))
            OnEntityCollision(entity, false);
    }

    // Called when the trigger is entered and stays. This is used for tracking entities.
    private void OnTriggerStay(Collider other)
    {
        // Entity object.
        Entity entity;

        // Checks if the entity is an entity object. If it is, call the entity collision object.
        if (other.gameObject.TryGetComponent(out entity))
            OnEntityCollision(entity, true);
    }

    // Called when colliding with an entity.
    // isTrigger determines if the trigger collision caused it or not.
    public abstract void OnEntityCollision(Entity entity, bool isTrigger);

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
        // TODO: change priority based on how hungry the entity is.
        if (hungry)
            foodPriority = 3;
        else if (!hungry)
            foodPriority = 0;
    }

    // checks to see if the sheep is hungry.
    public bool IsHungry()
    {
        return nourishedValue < fullThreshold;
    }

    // Sets the food object.
    public void SetFood(GameObject newFood)
    {
        // Sets the new food.
        food = newFood;

        // TODO: change based on hunger level.
        if(food != null)
        {
            // Calculates the hunger value and sets the food priority.
            CalculateHunger();
        }
        else
        {
            foodPriority = 0;
        }
    }

    // Sets threat for the object to retreat from.
    public void SetThreat(GameObject newThreat)
    {
        // Sets the new threat.
        threat = newThreat;

        // Checks if the threat was set.
        if (threat != null)
        {
            threatPriority = 2;
        }
        else
        {
            threatPriority = 0;
        }
    }

    // used to make the animal reporduce.
    protected abstract void Reproduce();

    // kills the animal.
    public abstract void Kill();

    // called when a living entity dies, and passes it its killer.
    // if the entity itself is passed, then the entity died of natural causes.
    // if null is passed, then it is unknown what killed the entity (possibly forcibly terminated).
    public abstract void OnDeath(GameObject killer);

    // Resets the behaviour priorities.
    public void ResetSteeringBehaviours()
    {
        SetFood(null);
        SetThreat(null);
        wanderPriority = 0;
    }

    // STEERING BEHAVIOURS

    // Seek the set target.
    public void Seek()
    {
        Seek(food);
    }


    // Seek the provided target.
    public void Seek(GameObject target)
    {
        // Set target and speed.
        seek.target = target;
        seek.speed = speed;

        // Run behaviour.
        if(seek.target != null)
            seek.RunBehaviour();
    }

    // Pursue the set target.
    public void Pursue()
    {
        Pursue(food);
    }


    // Pursue the provided target.
    public void Pursue(GameObject target)
    {
        // Set target and speed - needs to grab the steering behaviour to find the target's rigidbody.
        // If it fails, use seek.
        if(target.TryGetComponent<SteeringBehaviour>(out pursue.target))
        {
            pursue.speed = speed;

            // Run behaviour.
            if (pursue.target != null)
                pursue.RunBehaviour();
        }
        else // Use seek instead.
        {
            Seek(target);
        }  
    }

    // Flee from the set threat.
    public void Flee()
    {
        Flee(threat);
    }

    // Flee from the provided threat.
    public void Flee(GameObject threat)
    {
        // Set target and speed.
        flee.target = threat;
        flee.speed = speed;

        // Run behaviour.
        if (flee.target != null)
            flee.RunBehaviour();
    }

    // Evade the set threat.
    public void Evade()
    {
        Evade(threat);
    }


    // Evade the provided threat.
    public void Evade(GameObject threat)
    {
        // Set target and speed - needs to grab the steering behaviour to find the target's rigidbody.
        // If it fails, use flee.
        if (threat.TryGetComponent<SteeringBehaviour>(out evade.target))
        {
            evade.speed = speed;

            // Run behaviour.
            if (evade.target != null)
                evade.RunBehaviour();
        }
        else // Use flee instead.
        {
            Flee(threat);
        }
    }

    // Run wander behaviour.
    public void Wander()
    {
        // Set speed, and run behaviour.
        wander.speed = speed;
        wander.RunBehaviour();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // increases age.
        if (aging)
            age += Time.deltaTime;

        // for giving birth. (TODO: have breeding component)
        {
            // increments birth timer.
            birthTimer += Time.deltaTime;

            // gives birth.
            if (birthTimer > birthTimeMax)
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

        // checking for death
        // TODO: adjust life expectancy based on different factors.
        {
            lifeExpect = lifeSpan;
        }


        // if the age has reached the life expectancy or life span
        if (age >= lifeExpect || age >= lifeSpan)
            OnDeath(gameObject); // has been killed.


        // STEERING BEHAVIOURS
        // Calculate how hungry the entity is.
        CalculateHunger();

        // Checks to find which behaviour has the highest value.
        int maxBehaviour = Mathf.Max(foodPriority, threatPriority, wanderPriority);


        // If food should be prioritized, and the entity is hungry.
        if(maxBehaviour == foodPriority && food != null && IsHungry())
        {
            // Tries to seek the food.
            Seek();
        }
        // Prioritize escaping the threat.
        else if(maxBehaviour == threatPriority && threat != null)
        {
            Flee();
        }
        else // Wander
        {
            Wander();
        }
    }

    // on destroy
    private void OnDestroy()
    {

    }
}
