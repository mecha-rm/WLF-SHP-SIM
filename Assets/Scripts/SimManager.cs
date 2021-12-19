using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// management for the simulator.
public class SimManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
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
