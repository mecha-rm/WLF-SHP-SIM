using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    // the life span as a static object.
    // this is the same for all wolves.
    private static int LIFE_SPAN = 100;

    float time = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        species = "wolf";
    }

    // reproduces the wolf.
    protected override void Reproduce()
    {
        Wolf wolf = EntityManager.GetInstance().GetWolf();
        wolf.transform.position = transform.position;

        // TODO: change position.
        wolf.transform.position = transform.position + new Vector3(0.0F, 1.0F, 0.0F);
    }

    // wolf has killed something.
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

        time += Time.deltaTime;
        if(time > 5.0F)
        {
            Reproduce();
            time = 0.0F;
        }
            
    }
}
