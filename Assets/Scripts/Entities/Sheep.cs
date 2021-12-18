using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the sheep object.
public class Sheep : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        // if the speices has not been set.
        if(species == "")
            species = "sheep";

        // entity name has not been set.
        if (entityName == "")
            entityName = "Sheep";

        // the description has not been set.
        if (description == "")
            description = "Prey species that feeds on grass.";

    }
    
    // reproduces the sheep.
    protected override void Reproduce()
    {
        Sheep sheep = EntityManager.GetInstance().GetSheep();
        sheep.transform.position = transform.position;

        // TODO: change position.
        sheep.transform.position = transform.position + new Vector3(0.0F, 1.0F, 0.0F);
    }

    // sheep has killed something (eaten grass).
    public override void Kills(GameObject victim)
    {
        // throw new System.NotImplementedException();
    }

    // sheep has been killed.
    public override void OnKilled(GameObject killer)
    {
        // throw new System.NotImplementedException();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
