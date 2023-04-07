using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the grass entity. For the purposes of 
public class Grass : Entity
{
    // the model for the grass's base stage.
    public GameObject stage0Model;

    // the model for the grass's final stage.
    public GameObject stage1Model;

    // how long the grass has been growing for.
    public float growthTime = 10.0F;

    // the amount of time it takes for the grass to grow.
    // at this stage, the grass can be eaten.
    public float growthTimeMax = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        // if the entity name is blank.
        if(entityName == "")
            entityName = "Grass";

        // if the description is blank.
        if(description == "")
            description = "a section of grass that replenishes after a certain period of time.";


        // switch model based on growth value.
        if (stage0Model != null && stage1Model != null)
        {
            // fully grown.
            if(growthTime >= growthTimeMax)
            {
                stage0Model.SetActive(false);
                stage1Model.SetActive(true);
            }
            else // still growing.
            {
                stage1Model.SetActive(false);
                stage0Model.SetActive(true);
            }
            
        }
    }

    // if this full grown grass?
    // TODO: for the future, change this to "IsEditable()" so that the grass size can be taken into account.
    public bool IsEdible()
    {
        return growthTime >= growthTimeMax;
    }

    // eats the grass, resetting it back to its beginning stage.
    public void Eaten()
    {
        // reset growth time
        growthTime = 0.0F;

        // switch model.
        if (stage0Model != null && stage1Model != null)
        {
            stage1Model.SetActive(false);
            stage0Model.SetActive(true);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // if the grass hasn't completely grown yet.
        if(growthTime < growthTimeMax)
        {
            growthTime += Time.deltaTime;

            // if maximum growth time has been reached.
            if (growthTime >= growthTimeMax)
            {
                growthTime = growthTimeMax;

                // makes the right model visible.
                stage0Model.SetActive(false);
                stage1Model.SetActive(true);
            }
                
        }
    }
}
