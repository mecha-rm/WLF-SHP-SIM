using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manages the different entities.
// TODO: maybe make this not a singleton?
public class EntityManager
{
    // instance of singleton
    private static EntityManager instance = null;

    // the sheep poool
    private Queue<Sheep> sheepPool = new Queue<Sheep>();
    private string sheepPrefab = "Prefabs/Entities/Sheep";

    // the wolf pool
    private Queue<Wolf> wolfPool = new Queue<Wolf>();
    private string wolfPrefab = "Prefabs/Entities/Wolf";

    // list of items in the pool
    private Queue<Grass> grassPool = new Queue<Grass>();
    private string grassPrefab = "Prefabs/Entities/Grass";

    // constructor
    private EntityManager()
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // gets the instance
    public static EntityManager GetInstance()
    {
        // no instance generated
        if (instance == null)
        {
            // generates instance
            instance = new EntityManager();
        }

        return instance;
    }

    // ENTITIES //
    // gets the entity
    public Entity GetEntity<T>()
    {
        // gets entity
        if(typeof(T) == typeof(Sheep)) // sheep
        {
            return GetSheep();
        }
        else if (typeof(T) == typeof(Wolf)) // wolf
        {

        }
        else if (typeof(T) == typeof(Grass)) // grass
        {

        }

        return null;
    }

    // SHEEP //
    // creates sheep
    public Sheep CreateSheep()
    {
        GameObject res = GameObject.Instantiate((GameObject) Resources.Load(sheepPrefab));
        // TODO: change settings of sheep
        return res.GetComponent<Sheep>();
    }


    // gets sheep from pool.
    public Sheep GetSheep()
    {
        // sheep entity
        Sheep entity = null;

        // sheep in pool
        if(sheepPool.Count > 0) // get sheep from pool
        {
            entity = sheepPool.Dequeue();
        }
        else // no sheep available, so make a new one.
        {
            entity = CreateSheep();
        }

        return entity;
    }

    // puts sheep back in pool.
    public void ReturnSheep(Sheep entity)
    {
        if (entity != null)
            sheepPool.Enqueue(entity);
    }
    
    // clears the sheep pool.
    public void ClearSheepPool()
    {
        sheepPool.Clear();
    }

    // WOLF //
    // creates wolf
    public Wolf CreateWolf()
    {
        GameObject res = GameObject.Instantiate((GameObject)Resources.Load(wolfPrefab));
        // TODO: change settings of wolf
        return res.GetComponent<Wolf>();
    }


    // gets sheep from pool.
    public Wolf GetWolf()
    {
        // grass entity
        Wolf entity = null;

        // wolf in pool
        if (wolfPool.Count > 0) // get sheep from pool
        {
            entity = wolfPool.Dequeue();
        }
        else // no sheep available, so make a new one.
        {
            entity = CreateWolf();
        }

        return entity;
    }

    // puts wolf back in pool.
    public void ReturnWolf(Wolf entity)
    {
        if (entity != null)
            wolfPool.Enqueue(entity);
    }

    // clears the wolf pool.
    public void ClearWolfPool()
    {
        wolfPool.Clear();
    }


    // GRASS //
    // creates grass
    public Grass CreateGrass()
    {
        GameObject res = GameObject.Instantiate((GameObject)Resources.Load(grassPrefab));
        // TODO: change settings of sheep
        return res.GetComponent<Grass>();
    }


    // gets sheep from pool.
    public Grass GetGrass()
    {
        // grass entity
        Grass entity = null;

        // grass in pool
        if (grassPool.Count > 0) // get grass from pool
        {
            entity = grassPool.Dequeue();
        }
        else // no sheep available, so make a new one.
        {
            entity = CreateGrass();
        }

        return entity;
    }

    // puts sheep back in pool.
    public void ReturnGrass(Grass entity)
    {
        if (entity != null)
            grassPool.Enqueue(entity);
    }

    // clears the sheep pool.
    public void ClearGrassPool()
    {
        grassPool.Clear();
    }
}
