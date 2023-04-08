using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// management for the simulator.
public class SimManager : MonoBehaviour
{
    // instance of singleton
    private static SimManager instance = null;

    [Header("User Interface")]
    // rate for updating UI
    // public float updateRate = 1.0F;
    // 
    // // the update timer.
    // public float updateTimer = 0.0F;

    // entity counts
    public int grassCount;
    public int sheepCount;
    public int wolfCount;

    // text objects.
    public Text grassCountText;
    public Text sheepCountText;
    public Text wolfCountText;

    // constructor
    private SimManager()
    {
        // ...
    }

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Instance not set.
        if (instance == null)
        {
            instance = this;
        }
        else // Instance already set.
        {
            // Only one instance allowed.
            if (instance != this)
                Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // gets the instance
    public static SimManager GetInstance()
    {
        // no instance generated
        if (instance == null)
        {
            // Generates instance.
            GameObject newObject = new GameObject("Sim Manager (singleton)");
            instance = newObject.AddComponent<SimManager>();
        }

        return instance;
    }

    // updates the canvas.
    public void UpdateCanvas()
    {
        // grass
        {
            grassCount = 0;

            // finds all grass patches
            Grass[] activeGrass = FindObjectsOfType<Grass>(false);

            // goes through all plants.
            foreach(Grass grass in activeGrass)
            {
                // if the grass is edible, it is counted.
                if (grass.IsEdible())
                    grassCount++;
            }

        }

        // sheep
        {
            sheepCount = 0;

            // finds all sheep.
            Sheep[] activeSheep = FindObjectsOfType<Sheep>(false);

            // goes through all sheep.
            foreach (Sheep sheep in activeSheep)
            {
                sheepCount++;
            }

        }

        // wolves
        {
            wolfCount = 0;

            // finds all wolves.
            Wolf[] activeWolves = FindObjectsOfType<Wolf>(false);

            // goes through all wolves.
            foreach (Wolf wolf in activeWolves)
            {
                wolfCount++;
            }

        }

        // sets all the text.
        // grass count.
        if(grassCountText != null)
            grassCountText.text = "Grass: " + grassCount.ToString("D3");

        // sheep count
        if(sheepCountText != null)
            sheepCountText.text = "Sheep: " + sheepCount.ToString("D3");

        // wolf count
        if(wolfCountText != null)
            wolfCountText.text = "Wolves: " + wolfCount.ToString("D3");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvas();
    }
}
