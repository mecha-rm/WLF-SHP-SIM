using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manages the different entities.
// TODO: maybe make this not a singleton?
public class EntityManager : MonoBehaviour
{
    // instance of singleton
    private static EntityManager instance = null;

    // the sheep poool
    public GameObject sheepPrefab;
    public Queue<Sheep> sheepPool = new Queue<Sheep>();
    public string sheepPrefabRes = "Prefabs/Entities/Sheep";

    // the wolf pool
    public GameObject wolfPrefab;
    public Queue<Wolf> wolfPool = new Queue<Wolf>();
    public string wolfPrefabRes = "Prefabs/Entities/Wolf";

    // list of items in the pool
    public GameObject grassPrefab;
    public Queue<Grass> grassPool = new Queue<Grass>();
    public string grassPrefabRes = "Prefabs/Entities/Grass";

    // constructor
    private EntityManager()
    {
        // ...
    }

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Instance not set.
        if(instance == null)
        {
            instance = this;
        }
        else // Instance already set.
        {
            // Only one instance allowed.
            if(instance != this)
                Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sheep not set.
        if(sheepPrefab == null)
            sheepPrefab = (GameObject)Resources.Load(sheepPrefabRes);

        // Wolf not set.
        if(wolfPrefab == null)
            wolfPrefab = (GameObject)Resources.Load(wolfPrefabRes);

        // Grass not set.
        if(grassPrefab == null)
            grassPrefab = (GameObject)Resources.Load(grassPrefabRes);
    }

    // gets the instance
    public static EntityManager GetInstance()
    {
        // no instance generated
        if (instance == null)
        {
            // Generates instance.
            GameObject newObject = new GameObject("Entity Manager (singleton)");
            instance = newObject.AddComponent<EntityManager>();
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

    // Returns the entity.
    public void ReturnEntity(Entity entity)
    {
        // Entity set.
        if (entity != null)
        {
            // Reset behaviours if the entity is an animal.
            if(entity is Animal)
                ((Animal)entity).ResetSteeringBehaviours();

            if (entity is Grass)
                grassPool.Enqueue((Grass)entity);
            else if (entity is Sheep)
                sheepPool.Enqueue((Sheep)entity);
            else if (entity is Wolf)
                wolfPool.Enqueue((Wolf)entity);


            // Deactivate the entity.
            entity.gameObject.SetActive(false);
        }
    }

    // SHEEP //
    // creates sheep
    public Sheep CreateSheep()
    {
        GameObject res = GameObject.Instantiate(sheepPrefab);
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

        // TODO: add reset function.
        entity.age = 0;
        entity.nourishedValue = entity.nourishedMax;

        entity.gameObject.SetActive(true);
        return entity;
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
        GameObject res = GameObject.Instantiate(wolfPrefab);
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

        // TODO: add reset function.
        entity.age = 0;
        entity.nourishedValue = entity.nourishedMax;

        entity.gameObject.SetActive(true);
        return entity;
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
        GameObject res = GameObject.Instantiate(grassPrefab);
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

        // TODO: add reset function.
        // Set growth time to max.
        entity.growthTime = entity.growthTimeMax - 1;

        entity.gameObject.SetActive(true);
        return entity;
    }

    // clears the sheep pool.
    public void ClearGrassPool()
    {
        grassPool.Clear();
    }
}
