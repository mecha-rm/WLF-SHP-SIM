using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    // the life span as a static object.
    // this is the same for all wolves.
    private static int LIFE_SPAN = 100;

    // Start is called before the first frame update
    void Start()
    {
        species = "wolf";
    }

    // reproduces the wolf.
    private void Reproduce()
    {

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
    }
}
