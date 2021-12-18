using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the wolf object.
public class Wolf : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        // species
        species = "Wolf";

        // entity name has not been set.
        if (entityName == "")
            entityName = "Wolf";

        // the description has not been set.
        if (description == "")
            description = "Predatory species that kills sheep.";
    }

    // reproduces the wolf.
    protected override void Reproduce()
    {
        Wolf wolf = EntityManager.GetInstance().GetWolf();
        wolf.transform.position = transform.position;

        // TODO: change position.
        wolf.transform.position = transform.position + new Vector3(0.0F, 1.0F, 0.0F);
    }

    // wolf has killed something (sheep)
    public override void Kills(GameObject victim)
    {
        // throw new System.NotImplementedException();
    }

    // wolf has been killed.
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
