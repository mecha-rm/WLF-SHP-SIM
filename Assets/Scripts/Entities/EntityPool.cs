using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPool
{
    // instance of singleton
    private static EntityPool instance = null;

    // list of items in the pool
    Queue<Grass> grassPool = new Queue<Grass>();

    // constructor
    private EntityPool()
    {
        Start();
    }

    // gets the instance
    public static EntityPool GetInstance()
    {
        // no instance generated
        if (instance == null)
        {
            // generates instance
            instance = new EntityPool();
        }

        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
}
